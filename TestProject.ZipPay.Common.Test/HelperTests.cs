using TestProject.ZipPay.Common.HelperMethod;

namespace TestProject.ZipPay.Common.Test
{
    [TestFixture]
    public class HelperTests
    {
        [Test]
        public void Should_Get_Full_Name()
        {
            //Arrange
            var firstName = "FirstName";
            var middleName = "MiddleName";
            var lastName = "LastName";

            var fullName = $"{firstName} {middleName} {lastName}";

            //Act
            var result = Helper.GetFullName(firstName, middleName, lastName);

            //Assert
            Assert.That(fullName, Is.EqualTo(result));
        }

        [Test]
        public void Should_Get_FirstName_LastName()
        {
            //Arrange
            var firstName = "FirstName";
            var middleName = "";
            var lastName = "LastName";

            var firstNameLastName = $"{firstName} {lastName}";

            //Act
            var result = Helper.GetFullName(firstName, middleName, lastName);

            //Assert
            Assert.That(firstNameLastName, Is.EqualTo(result));
        }

        [Test]
        public void Should_Get_EmptyString()
        {
            //Act
            var result = Helper.GetFullName("", "", "");

            //Assert
            Assert.That("", Is.EqualTo(result));
        }

        [Test]
        public void Should_ValidateMinCreditLimit_Return_False()
        {
            //Act
            var result = Helper.ValidateMinCreditLimit(2000, 100);

            //Assert
            Assert.That(false, Is.EqualTo(result));
        }

        [Test]
        public void Should_ValidateMinCreditLimit_Return_True()
        {
            //Act
            var result = Helper.ValidateMinCreditLimit(2000, 1800);

            //Assert
            Assert.IsTrue(result);
        }
    }
}