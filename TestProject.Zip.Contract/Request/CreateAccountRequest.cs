using System.ComponentModel.DataAnnotations;

namespace TestProject.ZipPay.Contract.Request
{
    /// <summary>
    /// Class use to map client json to class type
    /// Class declares property to create new user account
    /// </summary>
    public class CreateAccountRequest
    {
        /// <summary>
        /// User email id
        /// </summary>
        [Required(ErrorMessage ="Email id is required.")]
        public string Email { get; set; }
    }
}
