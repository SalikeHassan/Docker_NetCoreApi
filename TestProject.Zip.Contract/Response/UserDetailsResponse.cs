using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.ZipPay.Contract.Response
{
    /// <summary>
    /// Class declares the properties of user details
    /// Response of api request
    /// </summary>
    public class UserDetailsResponse
    {
        /// <summary>
        /// User id, primary key value
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Full name of the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email id of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Monthly salary of the user
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Monthly expense of the user
        /// </summary>
        public decimal Expense { get; set; }

        /// <summary>
        /// Gender of the user
        /// </summary>
        public string Gender { get; set; }
    }
}
