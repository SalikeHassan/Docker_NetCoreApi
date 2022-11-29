using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestProject.ZipPay.Domain.BaseEntity;

namespace TestProject.ZipPay.Domain.Entities
{
    /// <summary>
    /// Entity class for account
    /// </summary>
    public class Account : Entity
    {
        /// <summary>
        /// Account number
        /// Required
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Account create date time: UTC time format
        /// Required
        /// </summary>
        public DateTimeOffset AccountCreateDateTime { get; set; }

        /// <summary>
        /// Account type
        /// Required
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Foreign key
        /// </summary>
        public int UserId { get; set; }
    }
}
