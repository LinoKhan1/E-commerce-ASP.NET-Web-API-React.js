using Moq;
using Xunit;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using e_commerce.Server.DTOs;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.Services;
using Microsoft.AspNetCore.Http;

public class AuthServiceTests
{
    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
    private readonly Mock<SignInManager<IdentityUser>> _signInManagerMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly IAuthService _authService;

    public AuthServiceTests()
    {
        _userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
        _signInManagerMock = new Mock<SignInManager<IdentityUser>>(_userManagerMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(), null, null, null, null);
        _jwtServiceMock = new Mock<IJwtService>();
        _authService = new AuthService(_userManagerMock.Object, _signInManagerMock.Object, _jwtServiceMock.Object);
    }

    [Fact]
    public async Task RegisterAsync_ShouldReturnSuccessResult_WhenUserIsCreated()
    {
        // Arrange
        var registerDto = new RegisterDTO { Username = "testuser", Password = "Password123!", Email = "test@example.com" };
        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                        .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _authService.RegisterUserAsync(registerDto);

        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var loginDto = new LoginDTO { Username = "testuser", Password = "Password123!" };
        _signInManagerMock.Setup(sm => sm.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false))
                          .ReturnsAsync(SignInResult.Success);
        _userManagerMock.Setup(um => um.FindByNameAsync(loginDto.Username))
                        .ReturnsAsync(new IdentityUser { UserName = loginDto.Username });
        _jwtServiceMock.Setup(js => js.GenerateJwtToken(It.IsAny<IdentityUser>()))
                       .Returns("valid-token");

        // Act
        var token = await _authService.LoginUserAsync(loginDto);

        // Assert
        Assert.Equal("valid-token", token);
    }
}
