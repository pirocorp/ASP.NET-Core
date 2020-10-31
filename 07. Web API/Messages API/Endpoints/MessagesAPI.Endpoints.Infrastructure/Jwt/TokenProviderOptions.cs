namespace MessagesAPI.Endpoints.Infrastructure.Jwt
{
    using System;

    using Microsoft.IdentityModel.Tokens;

    public class TokenProviderOptions
    {
        public TokenProviderOptions()
        {
            this.Path = "/api/users/login";
            this.Expiration = TimeSpan.FromDays(15);
        }

        public string Path { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }

}
