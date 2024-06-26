using e_commerce.Server.Models;
using e_commerce.Server.Services;
using e_commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace e_commerce.Tests
{
    public class JwtServiceTests
    {
        private readonly IJwtService _jwtService;

        public JwtServiceTests()
        {
            // Arrange JwtSettings
            var jwtSettings = new JwtSettings
            {
                Key = "3018cyREang9m7J+PTV19QJLIS9PFnabCdJKa0hZgj0=",
                Issuer = "localhost",
                Audience = "localhost",
                ExpireHours = 1
            };

            var options = Options.Create(jwtSettings);

            // Create JwtService
            _jwtService = new JwtService(options);
        }
        [Fact]
        public void GenerateJwtToken_ShouldReturnToken_WhenUserIsValid()
        {
            // Arrange
            var user = new IdentityUser { UserName = "testuser", Id = "123" };

            // Act
            var token = _jwtService.GenerateJwtToken(user);

            // Assert
            Assert.NotNull(token);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token) as JwtSecurityToken;
            var userNameClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            Assert.Equal("testuser", userNameClaim);
        }


        [Fact]
        public void VerifyJwtToken_ShouldReturnTrue_WhenTokenIsValid()
        {
            // Arrange
            var user = new IdentityUser { UserName = "testuser" };
            var token = _jwtService.GenerateJwtToken(user);

            // Act
            var isValid = _jwtService.VerifyJwtToken(token);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void VerifyJwtToken_ShouldReturnFalse_WhenTokenIsInvalid()
        {
            // Arrange
            var invalidToken = "invalid.token.here";

            // Act
            var isValid = _jwtService.VerifyJwtToken(invalidToken);

            // Assert
            Assert.False(isValid);
        }
    }
}
