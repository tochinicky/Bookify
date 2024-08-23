using FluentAssertions;

namespace Bookify.Domain.UnitTests;

public class UserTests
{
    [Fact]
    public void Create_Should_SetPropertyValues()
    {
        //Act
        var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);
        //Assert
        user.FirstName.Should().Be(user.FirstName);
        user.LastName.Should().Be(user.LastName);
        user.Email.Should().Be(user.Email);
    }
}
