using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.ZipPay.Command.AccountCommand;
using TestProject.ZipPay.Common;
using TestProject.ZipPay.Contract.Request;
using TestProject.ZipPay.Contract.Response;
using TestProject.ZipPay.Query.AccountQuery;
using TestProject.ZipPay.Query.UserQuery;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Serilog;

namespace TestProject.ZipPay.Api.Controllers
{
    /// <summary>
    /// Controller class for account api
    /// Version 1.0
    /// </summary>
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<AccountController> logger;
        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Api returns all the active users accounts
        /// Api supports pagination
        /// </summary>
        /// <param name="pageNum">Page Number</param>
        /// <param name="pageSize">Count of Data to Display in Current Page</param>
        /// <returns></returns>
        [HttpGet("{pageNum:int}/{pageSize:int}")]
        [ProducesResponseType(typeof(List<AccountDetailsResponse>), Status200OK)]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<IActionResult> Get(int pageNum, int pageSize)
        {
            try
            {
                //Get account query is called using mediator to get active users account available in the database table
                var data = await this.mediator.Send(new GetAccountDetailsQuery() { pageNum = pageNum, pageSize = pageSize });

                if (!data.Any())
                {
                    this.logger.LogInformation(Constant.NoAccountLogMsg);
                    //Sending no content status when database table either is not having any users account records or the available users account records are not in active state
                    return this.NoContent();
                }

                else
                {
                    var response = new PaginationResponse<AccountDetailsResponse>()
                    {
                        PageNum = pageNum,
                        PageSize = pageSize,
                        Data = data,
                        Total = data.Count
                    };
                    return this.Ok(response);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                return this.StatusCode(500);
            }
        }

        /// <summary>
        /// Api returns the user account details based on the user id passed
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AccountDetailsResponse), Status200OK)]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                //Get user account by id query is called using mediator to get the user account details
                var data = await this.mediator.Send(new GetAccountDetailsByIdQuery() { id = id });

                if (data != null)
                {
                    return this.Ok(data);
                }

                else
                {
                    this.logger.LogInformation($"{Constant.NoAccountByIdLogMsg}{id}");
                    //When user account is not available in database table, sending not found status code
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                return this.StatusCode(500);
            }
        }

        /// <summary>
        /// Api creates a new user account in the data table when model validation pass
        /// </summary>
        /// <param name="createAccountRequest">Object contains the data to create the new user account</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateAccountRequest createAccountRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //Sending bad request status to client when request model validation failed
                    return this.BadRequest();
                }

                //Get the user details based on the email id
                var data = await this.mediator.Send(new GetUserByEmailQuery() { email = createAccountRequest.Email });

                if (data != null)
                {
                    if (data.Salary - data.Expense < Constant.MinCreditLimit)
                    {
                        //Sending bad request when user  monthly salary - expense is less than 1000
                        //User is not allowed to create the account
                        return this.BadRequest(Constant.AccountErrorMsg);
                    }

                    //Create user account when model is valid and the difference of user salary and expense is greater than 1000
                    await this.mediator.Send(new CreateAccountCommand() { createAccountRequest = createAccountRequest, userId = data.Id });

                    return this.Ok();
                }
                else
                {
                    //When user is not available in database table, sending not found status code
                    return this.BadRequest(Constant.UserRegisterErrorMsg);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                return this.StatusCode(500);
            }
        }
    }
}
