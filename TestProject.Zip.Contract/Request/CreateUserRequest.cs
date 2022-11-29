using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TestProject.ZipPay.Contract.Request
{
    /// <summary>
    /// Class use to map client json to class type
    /// Class declares property to create new user
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// First name of user
        /// Required field
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Middle name of user
        /// Optional field
        /// </summary>
        [AllowNull]
        public string MiddleName { get; set; }

        /// <summary>
        /// Last name of user
        /// Required field
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Emaul id of user
        /// Required field
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User monthly salary
        /// Required field
        /// </summary>
        [Required]
        public decimal Salary { get; set; }

        /// <summary>
        /// User monthly expense
        /// Required field
        /// </summary>
        [Required]
        public decimal Expense { get; set; }

        /// <summary>
        /// Gender of user
        /// Required field
        /// </summary>
        [Required]
        public string Gender { get; set; }
    }
}
