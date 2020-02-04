using System.Collections.Generic;
using System.Threading.Tasks;
using blogapi.Contracts.Responses;
using blogapi.Models;

namespace blogapi.Contracts
{
    public interface IUserRepository
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}