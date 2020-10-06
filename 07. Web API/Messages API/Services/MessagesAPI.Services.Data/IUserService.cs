namespace MessagesAPI.Services.Data
{
    using MessagesAPI.Data.Models;

    public interface IUserService
    {
        User GetByName(string username);

        void Register(string username, string password);

        string Login(string username, string password);
    }
}
