namespace TestProject.ZipPay.Common
{
    /// <summary>
    /// Constant class defines the constants used accross the projects
    /// </summary>
    public static class Constant
    {
        public const string AccountErrorMsg = "You are not elegibel to create the account.";

        public const string UserRegisterErrorMsg = "User is not registered.";

        public const int MinCreditLimit = 1000;

        public const string EmailErrorMsg = "Email is already in use.";

        public const string AccountType = "Credit";

        public const string NoAccountLogMsg = "No account available";

        public const string NoAccountByIdLogMsg = "Account not found for id: ";

        public const string NoUserLogMsg = "No user available";

        public const string NoUserByIdLogMsg = "User not found for id: ";
    }
}
