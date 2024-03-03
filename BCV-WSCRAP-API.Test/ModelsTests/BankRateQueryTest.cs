using BCV_WSCRAP_API.Models;
using FluentAssertions;

namespace BCV_WSCRAP_API.Test.ModelsTests
{
    public class BankRateQueryTest
    {
        private const string BANK_CODE = "0000";

        #region [IsEmpty Method]
        [Fact]
        public void IsEmpty_NoMinimumDateNoMaximumDateNoBankCode_ReturnsTrue()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = null,
                BankCode = null,
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsEmpty_NoMinimumDateNoMaximumDateEmptyBankCode_ReturnsTrue()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = null,
                BankCode = string.Empty,
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsEmpty_NoMinimumDateNoMaximumDateButBankCode_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = null,
                BankCode = BANK_CODE,
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_NoMinimumDateButMaximumDateNoBankCode_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = DateTime.Now,
                BankCode = null
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_NoMinimumDateButMaximumDateEmptyBankCode_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = DateTime.Now,
                BankCode = string.Empty
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_NoMinimumDateButMaximumDateAndBankCode_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = DateTime.Now,
                BankCode = BANK_CODE
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_MinimumDateButNoMaximumDateNullBankCode_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = null,
                BankCode = null
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_MinimumDateButNoMaximumDateEmptyBankCode_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = null,
                BankCode = string.Empty
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_MinimumDateButNoMaximumDateAndHasBankCode_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = null,
                BankCode = BANK_CODE
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_MinimumDateAndMaximumDateNullBankcode_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = DateTime.Now,
                BankCode = null
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_MinimumDateAndMaximumDateEmptyBankCode_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = DateTime.Now,
                BankCode = string.Empty
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_MinimumDateAndMaximumDateAndBankCode_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = DateTime.Now,
                BankCode = BANK_CODE
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }
        #endregion

        #region [HasOnlyMinimumDate Method]
        [Fact]
        public void HasOnlyMinimumDate_NoMinimumDateNoMaximumDate_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = null,
            };

            //Act
            var result = query.HasOnlyMinimumDate();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void HasOnlyMinimumDate_NoMinimumDateButMaximumDate_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = DateTime.Now,
            };

            //Act
            var result = query.HasOnlyMinimumDate();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void HasOnlyMinimumDate_MinimumDateButNoMaximumDate_ReturnsTrue()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = null,
            };

            //Act
            var result = query.HasOnlyMinimumDate();

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void HasOnlyMinimumDate_MinimumDateAndMaximumDate_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = DateTime.Now,
            };

            //Act
            var result = query.HasOnlyMinimumDate();

            //Assert
            result.Should().BeFalse();
        }
        #endregion

        #region [HasOnlyMaximumDate Method]
        [Fact]
        public void HasOnlyMaximumDate_NoMinimumDateNoMaximumDate_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = null,
            };

            //Act
            var result = query.HasOnlyMaximumDate();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void HasOnlyMaximumDate_NoMinimumDateButMaximumDate_ReturnsTrue()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = DateTime.Now,
            };

            //Act
            var result = query.HasOnlyMaximumDate();

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void HasOnlyMaximumDate_MinimumDateButNoMaximumDate_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = null,
            };

            //Act
            var result = query.HasOnlyMaximumDate();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void HasOnlyMaximumDate_MinimumDateAndMaximumDate_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = DateTime.Now,
            };

            //Act
            var result = query.HasOnlyMaximumDate();

            //Assert
            result.Should().BeFalse();
        }
        #endregion

        #region [HasNoDates Method]
        [Fact]
        public void HasNoDates_NoMinimumDateNoMaximumDate_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = null,
            };

            //Act
            var result = query.HasNoDates();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void HasNoDates_NoMinimumDateButMaximumDate_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = DateTime.Now,
            };

            //Act
            var result = query.HasNoDates();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void HasNoDates_MinimumDateButNoMaximumDate_ReturnsFalse()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = null,
            };

            //Act
            var result = query.HasNoDates();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void HasNoDates_MinimumDateAndMaximumDate_ReturnsTrue()
        {
            //Arrange
            BankRateQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = DateTime.Now,
            };

            //Act
            var result = query.HasNoDates();

            //Assert
            result.Should().BeTrue();
        }

        #endregion
    }
}
