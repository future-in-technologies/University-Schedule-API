using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UniversitySchedule.Core.Entites;

namespace UniversitySchedule.Core.Abstractions.OperationInterfaces
{
    public interface IUserOperations
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        User GetById(int id);
        Task<User> CreateAsync(User user, string password);
        Task UpdateAsync(User user, string password = null);
        Task DeleteAsync(int id);
    }
}
