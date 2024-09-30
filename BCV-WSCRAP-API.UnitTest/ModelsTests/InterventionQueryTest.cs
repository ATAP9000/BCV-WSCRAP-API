using BCV_WSCRAP_API.Models;
using FluentAssertions;

namespace BCV_WSCRAP_API.Test.ModelsTests
{
    public class InterventionQueryTest
    {
        private const string BINTERVENTION_CODE = "000-00";

        #region [IsEmpty Method]
        [Fact]
        public void IsEmpty_QueryWithNullProperties_ReturnsTrue()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = null,
                InterventionCode = null,
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsEmpty_QueryWithoutDatesAndEmptyCode_ReturnsTrue()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = null,
                InterventionCode = string.Empty,
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsEmpty_QueryWithWithCode_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = null,
                InterventionCode = BINTERVENTION_CODE,
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_QueryWithMaximumDate_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = DateTime.Now,
                InterventionCode = null
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_QueryWithMaximumDateAndEmptyCode_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = DateTime.Now,
                InterventionCode = string.Empty
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_QueryWithMaximiumDateAndCode_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = null,
                MaximumDate = DateTime.Now,
                InterventionCode = BINTERVENTION_CODE
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_QueryWithMinimumDate_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = null,
                InterventionCode = null
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_QueryWithMinimumDateAndEmptyCode_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = null,
                InterventionCode = string.Empty
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_QueryWithMinimumDateAndCode_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = null,
                InterventionCode = BINTERVENTION_CODE
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_QueryWithDates_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = DateTime.Now,
                InterventionCode = null
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_QueryWithDatesAndEmptyCode_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = DateTime.Now,
                InterventionCode = string.Empty
            };

            //Act
            var result = query.IsEmpty();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsEmpty_QueryWithDatesAndCode_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = DateTime.Now,
                MaximumDate = DateTime.Now,
                InterventionCode = BINTERVENTION_CODE
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
            InterventionQuery query = new()
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
            InterventionQuery query = new()
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
            InterventionQuery query = new()
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
            InterventionQuery query = new()
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
            InterventionQuery query = new()
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
            InterventionQuery query = new()
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
            InterventionQuery query = new()
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
            InterventionQuery query = new()
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
        public void HasNoDates_QueryWithoutDates_ReturnsTrue()
        {
            //Arrange
            InterventionQuery query = new()
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
        public void HasNoDates_QueryWithMaximumDate_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
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
            InterventionQuery query = new()
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
        public void HasNoDates_MinimumDateAndMaximumDate_ReturnsFalse()
        {
            //Arrange
            InterventionQuery query = new()
            {
                MinimumDate = DateTime.UtcNow,
                MaximumDate = DateTime.UtcNow,
            };

            //Act
            var result = query.HasNoDates();

            //Assert
            result.Should().BeFalse();
        }

        #endregion
    }
}
