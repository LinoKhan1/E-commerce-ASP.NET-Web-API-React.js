using e_commerce.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace e_commerce.Server.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(IdentityUser user);
        bool VerifyJwtToken(string token);
    }
}
