namespace ACAPackagesListener.API.Authentication
{
    public class HardCodeAuthenticator : IAuthenticate
    {
        public bool Authenticate(string username, string password)
        {
            return (username == "root" && password=="12345");
        }
    }
}
