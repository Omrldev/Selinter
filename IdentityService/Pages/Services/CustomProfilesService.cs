using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityService.Pages.Services
{
    public class CustomProfilesService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public CustomProfilesService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var existingClaim = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new("username", user.UserName)
                //new Claim("username", user.UserName)
            };

            context.IssuedClaims.AddRange(claims);
            context.IssuedClaims.Add(existingClaim
                .FirstOrDefault(x => x.Type == JwtClaimTypes.Name));
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
