using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.ZipPay.Contract.Response
{
    /// <summary>
    /// Class declares the properties of user account details
    /// Response of api request
    /// </summary>
    public class AccountDetailsResponse
    {
        /// <summary>
        /// Account Id, primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string AccountHolderName { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Account number, which was created during user account creation
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Account type of user
        /// </summary>
        public string AccountType { get; set; }
    }
}
