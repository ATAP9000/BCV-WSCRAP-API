using BCV_WSCRAP_API.Models;
using FluentAssertions;

namespace BCV_WSCRAP_API.Test.ModelsTests
{
    public class ExchangeRateQueryTest
    {
        #region [HasOnlyMinimumDate Method]
        [Fact]
        public void HasOnlyMinimumDate_NoMinimumDateNoMaximumDate_ReturnsFalse()
        {
            //Arrange
            ExchangeRateQuery query = new()
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
            ExchangeRateQuery query = new()
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
            ExchangeRateQuery query = new()
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
            ExchangeRateQuery query = new()
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
            ExchangeRateQuery query = new()
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
            ExchangeRateQuery query = new()
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
            ExchangeRateQuery query = new()
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
            ExchangeRateQuery query = new()
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
            ExchangeRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = null,
            };

            //Act
            var result = query.HasNoDates();

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void HasNoDates_NoMinimumDateButMaximumDate_ReturnsFalse()
        {
            //Arrange
            ExchangeRateQuery query = new()
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
            ExchangeRateQuery query = new()
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
            ExchangeRateQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = null,
            };

            //Act
            var result = query.HasNoDates();

            //Assert
            result.Should().BeTrue();
        }

        #endregion
    }
}
