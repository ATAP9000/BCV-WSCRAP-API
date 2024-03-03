using BCV_WSCRAP_API.Models;
using BCV_WSCRAP_API.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BCV_WSCRAP_API.Test.ModelsTests
{
    public class BankRateTest
    {
        private const string BANK_NAME = "BankTest";
        private const string BANK_CODE = "0000";
        private const string NONEXISTING_BANK = "AnotherBank";

        [Fact]
        public void AssignBankCode_NullBankDictionary_DoesNothing()
        {
            //Arrange
            BankDictionary? bankDictionary = null;
            BankRate bankRate = new()
            {
                Bank = BANK_NAME
            };

            //Act
            bankRate.AssignBankCode(bankDictionary);

            //Assert
            bankRate.BankCode.Should().BeNullOrEmpty();
        }

        [Fact]
        public void AssignBankCode_EmptyDictionary_DoesNothing()
        {
            //Arrange
            var mockConfig = new Mock<IConfiguration>();
            BankDictionary? bankDictionary = new(mockConfig.Object)
            {
                BankCodes = []
            };
            BankRate bankRate = new()
            {
                Bank = BANK_NAME
            };

            //Act
            bankRate.AssignBankCode(bankDictionary);

            //    //Assert
            bankRate.BankCode.Should().BeNullOrEmpty();
        }


        [Fact]
        public void AssignBankCode_NotExistentKeyInDictionary_DoesNothing()
        {
            //Arrange
            var mockConfig = new Mock<IConfiguration>();
            BankDictionary bankDictionary = new(mockConfig.Object);
            bankDictionary.BankCodes.Add(BANK_NAME,BANK_CODE);
            BankRate bankRate = new()
            {
                Bank = NONEXISTING_BANK
            };


            //Act
            bankRate.AssignBankCode(bankDictionary);

            //Assert
            bankRate.BankCode.Should().BeNullOrEmpty();
        }

        [Fact]
        public void AssignBankCode_ExistentKeyInDictionary_AssignsBankCode()
        {
            //Arrange
            var mockConfig = new Mock<IConfiguration>();
            BankDictionary? bankDictionary = new(mockConfig.Object);
            bankDictionary.BankCodes.Add(BANK_NAME, BANK_CODE);
            BankRate bankRate = new()
            {
                Bank = BANK_NAME
            };


            //Act
            bankRate.AssignBankCode(bankDictionary);

            //Assert
            bankRate.BankCode.Should().NotBeNullOrEmpty();
        }

    }
}
