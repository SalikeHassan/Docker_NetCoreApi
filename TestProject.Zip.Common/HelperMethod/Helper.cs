using System.Text;

namespace TestProject.ZipPay.Common.HelperMethod
{
    /// <summary>
    /// Helper class for common functionality methods
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Method to get the full name
        /// </summary>
        /// <param name="firstName">First Name</param>
        /// <param name="middleName">Middle Name</param>
        /// <param name="lastName">Last Name</param>
        /// <returns>returns the full name</returns>
        public static string GetFullName(string firstName, string middleName, string lastName)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(firstName);

            if (!string.IsNullOrEmpty(middleName) && !string.IsNullOrWhiteSpace(middleName))
            {
                stringBuilder.Append(" ");
                stringBuilder.Append(middleName);
            }

            stringBuilder.Append(" ");
            stringBuilder.Append(lastName);

            return stringBuilder.ToString().Trim();
        }

        /// <summary>
        /// Validate minimum credit limit
        /// </summary>
        /// <param name="salary">User monthly salary</param>
        /// <param name="expense">User monthly expense</param>
        /// <returns></returns>
        public static bool ValidateMinCreditLimit(decimal salary,decimal expense)
        {
            return salary - expense < Constant.MinCreditLimit;
        }
    }
}
