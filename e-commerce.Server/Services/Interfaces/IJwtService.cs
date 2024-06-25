using e_commerce.Server.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace e_commerce.Server.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(ApplicationUser user);
        Task<bool> VerifyJwtToken(string token);
    }
}
