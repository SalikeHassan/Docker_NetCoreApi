using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.ZipPay.Command.UserCommand;
using TestProject.ZipPay.Common;
using TestProject.ZipPay.Contract.Request;
using TestProject.ZipPay.Contract.Response;
using TestProject.ZipPay.Query.UserQuery;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace TestProject.ZipPay.Api.Controllers
{
    /// <summary>
    /// Controller class for user api
    /// Version 1.0
    /// </summary>
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api returns all the active users
        /// Api supports pagination
        /// </summary>
        /// <param name="pageNum">Page Number</param>
        /// <param name="pageSize">Count of Data to Display in Current Page</param>
        /// <returns></returns>
        [HttpGet("{pageNum:int}/{pageSize:int}")]
        [ProducesResponseType(typeof(List<UserDetailsResponse>), Status200OK)]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<IActionResult> Get(int pageNum, int pageSize)
        {
            //Get user query is called using mediator to get active users available in the database table
            var data = await this.mediator.Send(new GetUsersQuery() { pageNum = pageNum, pageSize = pageSize });

            if (!data.Any())
            {
                //Sending no content status when database table is not having user records
                return this.NoContent();
            }

            else
            {
                var response = new PaginationResponse<UserDetailsResponse>()
                {
                    PageNum = pageNum,
                    PageSize = pageSize,
                    Data = data,
                    Total = await this.mediator.Send(new GetUserCountQuery())
            };
            return this.Ok(response);
        }
    }

    /// <summary>
    /// Api returns the user details based on the user id passed
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserDetailsResponse), Status200OK)]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        //Get user by id query is called using mediator to get the user details
        var data = await this.mediator.Send(new GetUserByIdQuery() { id = id });

        if (data != null)
        {
            return this.Ok(data);
        }

        else
        {
            //When user id is not available or user marked as inactive in database table, sending not found status code
            return this.NotFound();
        }
    }

    /// <summary>
    /// Api creates a new user in the data table when model validation pass
    /// </summary>
    /// <param name="createUserRequest">Object contains the data to create the new user</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest createUserRequest)
    {
        if (!ModelState.IsValid)
        {
            //Sending bad request status to client when request model validation failed
            return this.BadRequest();
        }

        //Getting user data based on the email id provided from client in request body
        var data = await this.mediator.Send(new GetUserByEmailQuery() { email = createUserRequest.Email });

        if (data != null)
        {
            //Sending bad request status to client when user data is found for the provided email id in the request body
            return this.BadRequest(Constant.EmailErrorMsg);
        }

        //Creating a new user when above model and unique email validation pass
        await this.mediator.Send(new CreateUserCommand() { createUserRequest = createUserRequest });
        return this.Ok();
    }
}
}
