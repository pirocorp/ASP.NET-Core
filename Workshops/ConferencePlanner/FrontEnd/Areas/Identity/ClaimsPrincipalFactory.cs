namespace FrontEnd.Areas.Identity
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    
    using Data;
    using Services;

    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        private readonly IApiClient apiClient;

        public ClaimsPrincipalFactory(
            IApiClient apiClient, 
            UserManager<User> userManager, 
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
            this.apiClient = apiClient;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            if (user.IsAdmin)
            {
                identity.MakeAdmin();
            }

            return identity;
        }
    }
}
