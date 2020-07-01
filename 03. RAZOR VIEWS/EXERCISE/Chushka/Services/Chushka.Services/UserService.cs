namespace Chushka.Services
{
    using System.Linq;
    using Contracts;
    using Data;

    public class UserService : IUserService
    {
        private readonly ChushkaDbContext _db;

        public UserService(ChushkaDbContext db)
        {
            this._db = db;
        }

        public bool Exists(string username)
            => this._db.Users.Any(u => u.UserName == username);

        public bool Any()
            => this._db.Users.Any();

        public int Count()
            => this._db.Users.Count();
    }
}
