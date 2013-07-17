namespace ACAPackagesListener.API.Authentication
{
    public interface IAuthenticate
    {
        bool Authenticate(string username, string password);
    }
}
