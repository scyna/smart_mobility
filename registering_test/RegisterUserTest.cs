using Xunit;
using scyna;
using Registering;

namespace registering_test;

public class RegisterUserTest : TestBase
{
    [Theory]
    [InlineData("b@gmail.com", "Nguyen Van B")]
    [InlineData("a@gmail.com", "Nguyen Van A")]
    public void TestRegisterUser_ShouldSuccess(string email, string name)
    {
        EndpointTest.Create(Path.REGISTER_USER_URL)
            .WithRequest(new Registering.PROTO.RegisterUserRequest
            {
                Email = email,
                Name = name,
                Password = "123456"
            })
            .ExpectEvent(new Registering.PROTO.RegistrationCreated
            {
                Email = email,
                Name = name,
            })
            .ExpectSuccess()
            .Run();
    }

    [Theory]
    [InlineData("a+gmail.com", "Nguyen Van A")]
    [InlineData("", "Nguyen Van A")]
    [InlineData("a@gmail.com", "")]
    [InlineData("a@gmail.com", "Very Long Name Should Return Invalid Very Long Name Should Return Invalid")]
    public void TestRegisterUser_ShouldReturnRequestInvalid(string email, string name)
    {
        EndpointTest.Create(Path.REGISTER_USER_URL)
            .WithRequest(new Registering.PROTO.RegisterUserRequest
            {
                Email = email,
                Name = name,
            })
            .ExpectError(scyna.Error.REQUEST_INVALID)
            .Run();
    }
}