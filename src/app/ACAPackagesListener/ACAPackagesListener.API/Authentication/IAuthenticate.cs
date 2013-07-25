using ACAPackagesListener.API.Models.Entities;

namespace ACAPackagesListener.API.Authentication
{
    public interface IAuthenticate
    {
        bool Authenticate(string username, string password);
        bool Authenticate(User user);
    }
}
