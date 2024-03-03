using BCV_WSCRAP_API.Models;
using FluentAssertions;

namespace BCV_WSCRAP_API.Test.ModelsTests
{
    public class InterventionQueryTest
    {
        private const string BINTERVENTION_CODE = "000-00";

        #region [IsEmpty Method]
        [Fact]
        public void IsEmpty_NoMinimumDateNoMaximumDateNoInterventionCode_ReturnsTrue()
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
        public void IsEmpty_NoMinimumDateNoMaximumDateEmptyInterventionCode_ReturnsTrue()
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
        public void IsEmpty_NoMinimumDateNoMaximumDateButInterventionCode_ReturnsFalse()
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
        public void IsEmpty_NoMinimumDateButMaximumDateNoInterventionCode_ReturnsFalse()
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
        public void IsEmpty_NoMinimumDateButMaximumDateEmptyInterventionCode_ReturnsFalse()
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
        public void IsEmpty_NoMinimumDateButMaximumDateAndInterventionCode_ReturnsFalse()
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
        public void IsEmpty_MinimumDateButNoMaximumDateNullInterventionCode_ReturnsFalse()
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
        public void IsEmpty_MinimumDateButNoMaximumDateEmptyInterventionCode_ReturnsFalse()
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
        public void IsEmpty_MinimumDateButNoMaximumDateAndHasInterventionCode_ReturnsFalse()
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
        public void IsEmpty_MinimumDateAndMaximumDateNullInterventionCode_ReturnsFalse()
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
        public void IsEmpty_MinimumDateAndMaximumDateEmptyInterventionCode_ReturnsFalse()
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
        public void IsEmpty_MinimumDateAndMaximumDateAndInterventionCode_ReturnsFalse()
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
        public void HasNoDates_NoMinimumDateNoMaximumDate_ReturnsFalse()
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
            result.Should().BeFalse();
        }

        [Fact]
        public void HasNoDates_NoMinimumDateButMaximumDate_ReturnsFalse()
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
        public void HasNoDates_MinimumDateAndMaximumDate_ReturnsTrue()
        {
            //Arrange
            InterventionQuery query = new()
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
