using Microsoft.AspNet.Identity.EntityFramework;

namespace IncidentReportingApi.Repository
{
    public interface ITokenRepository
    {
        Task<string> CreateJWTokenAsync(IdentityUser user, List<string> roles);
    }
}
