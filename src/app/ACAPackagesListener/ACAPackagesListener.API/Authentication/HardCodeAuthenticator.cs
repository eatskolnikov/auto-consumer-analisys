using ACAPackagesListener.API.Models.Entities;

namespace ACAPackagesListener.API.Authentication
{
    public class HardCodeAuthenticator : IAuthenticate
    {
        public bool Authenticate(string username, string password)
        {
            return (username == "root" && password == "12345");
        }
        public bool Authenticate(User user)
        {
            return (user.Username == "root" && user.Password == "12345");
        }
    }
}
