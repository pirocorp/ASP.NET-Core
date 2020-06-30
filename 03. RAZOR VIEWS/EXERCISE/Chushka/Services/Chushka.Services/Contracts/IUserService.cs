namespace Chushka.Services.Contracts
{
    public interface IUserService
    {
        bool Exists(string username);
    }
}
