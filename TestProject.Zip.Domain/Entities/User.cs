using TestProject.ZipPay.Domain.BaseEntity;

namespace TestProject.ZipPay.Domain.Entities
{
    /// <summary>
    /// Entity class for user
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// First name
        /// Required
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Middle name
        /// Optional
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Last name
        /// Required
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email id
        /// Required
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Monthly salary
        /// Required
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Monthly expense
        /// Required
        /// </summary>
        public decimal Expense { get; set; }

        /// <summary>
        /// Gender
        /// Required
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        ///Navigation property
        /// </summary>
        public virtual Account Account { get; set; }
    }
}
