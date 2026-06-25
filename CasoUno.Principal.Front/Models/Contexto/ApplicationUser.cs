using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CasoUno.Principal.Front.Models.Contexto
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim(DisplayName, this.DisplayName ?? ""));
            return userIdentity;
        }

    }
}