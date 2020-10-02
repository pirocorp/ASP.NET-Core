namespace ForumSystem.Web.Infrastructure.Tests
{
    using Attributes;
    using Xunit;

    public class EgnValidationAttributeTests
    {
        [Fact]
        public void Egn1234567890ShouldBeValid()
        {
            //Arrange
            var egnValidationAttribute = new EgnValidationAttribute();

            //Act
            var isValid = egnValidationAttribute.IsValid("1234567890");

            //Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1234567899")]
        [InlineData("asdfghjksd")]
        [InlineData("123456789")]
        [InlineData("\0")]
        [InlineData("          ")]
        public void EgnShouldBeInvalid(string egn)
        {
            //Arrange
            var attribute = new EgnValidationAttribute();

            //Act
            var isValid = attribute.IsValid(egn);

            //Assert
            Assert.False(isValid);
        }
    }
}
