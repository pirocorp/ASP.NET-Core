namespace MessagesAPI.Services.Data
{
    using System;
    using System.Linq;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Endpoints.Infrastructure.Jwt;
    using MessagesAPI.Data;
    using MessagesAPI.Data.Models;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class UserService : IUserService
    {
        private readonly MessagesDbContext db;
        private readonly JwtSettings jwtSettings;

        public UserService(MessagesDbContext db, IOptions<JwtSettings> jwtSettings)
        {
            this.db = db;
            this.jwtSettings = jwtSettings.Value;
        }

        public User GetByName(string username)
            => this.db.Users.SingleOrDefault(u => u.Username == username);

        public void Register(string username, string password)
        {
            var user = new User()
            {
                Username = username,
                Password = password,
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public string Login(string username, string password)
        {
            var user = this.db.Users
                .SingleOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Username), 
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
