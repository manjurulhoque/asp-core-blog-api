using System.Collections.Generic;
using blogapi.Models;

namespace blogapi.Contracts
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Register(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}