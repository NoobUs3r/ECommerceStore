using Moq;
using SharedService.Interfaces;
using SharedService.Models;

namespace UnitTests.API.Auth;

[TestClass]
public class AuthTests
{
    private Mock<IAuthService> _authServiceMock;

    [TestInitialize]
    public void Setup()
    {
        _authServiceMock = new Mock<IAuthService>();

        // Seed the in-memory responses for mock
        SeedAuthServiceMock();
    }

    [TestMethod]
    public async Task RegisterUser_ShouldReturnSuccessMessage_WhenUserIsRegistered()
    {
        var result = await _authServiceMock.Object.RegisterUser("newuser", "newpassword");

        Assert.AreEqual("User registered successfully.", result);
        _authServiceMock.Verify(auth => auth.RegisterUser(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [TestMethod]
    public async Task RegisterUser_ShouldReturnErrorMessage_WhenUsernameAlreadyExists()
    {
        var result = await _authServiceMock.Object.RegisterUser("testuser", "newpassword");

        Assert.AreEqual("Username already exists.", result);
        _authServiceMock.Verify(auth => auth.RegisterUser(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [TestMethod]
    public async Task AuthenticateUser_ShouldReturnJwtToken_WhenCredentialsAreValid()
    {
        var result = await _authServiceMock.Object.AuthenticateUser("testuser", "password");

        Assert.IsTrue(result.IsAuthenticated);
        Assert.AreEqual("valid-jwt-token", result.Jwt);
    }

    [TestMethod]
    public async Task AuthenticateUser_ShouldReturnFalse_WhenCredentialsAreInvalid()
    {
        var result = await _authServiceMock.Object.AuthenticateUser("invaliduser", "invalidpassword");

        Assert.IsFalse(result.IsAuthenticated);
        Assert.IsTrue(string.IsNullOrEmpty(result.Jwt));
    }

    private void SeedAuthServiceMock()
    {
        // Mocking RegisterUser behavior
        _authServiceMock.Setup(auth => auth.RegisterUser("newuser", "newpassword"))
            .ReturnsAsync("User registered successfully.");

        _authServiceMock.Setup(auth => auth.RegisterUser("testuser", "newpassword"))
            .ReturnsAsync("Username already exists.");

        // Mocking AuthenticateUser behavior
        _authServiceMock.Setup(auth => auth.AuthenticateUser("testuser", "password"))
            .ReturnsAsync(new AuthenticateUserResponse("valid-jwt-token", true));

        _authServiceMock.Setup(auth => auth.AuthenticateUser("invaliduser", "invalidpassword"))
            .ReturnsAsync(new AuthenticateUserResponse(string.Empty, false));
    }
}