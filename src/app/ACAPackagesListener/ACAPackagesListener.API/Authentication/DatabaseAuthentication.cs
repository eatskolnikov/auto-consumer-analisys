using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;

namespace ACAPackagesListener.API.Authentication
{
    public class DatabaseAuthentication : IAuthenticate
    {
        private readonly IUserRepository userRepository;

        public DatabaseAuthentication()
        {
            userRepository = new NHUserRepository();
        }

        public bool Authenticate(string username, string password)
        {
            return userRepository.Authenticate(username, password) != null;
        }

        public bool Authenticate(User user)
        {
            return userRepository.Authenticate(user.Username, user.Password) != null;
        }
    }
}
