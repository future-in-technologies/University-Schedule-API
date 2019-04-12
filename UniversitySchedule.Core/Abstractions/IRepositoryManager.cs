using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UniversitySchedule.Core.Abstractions;
using UniversitySchedule.Core.Abstractions.RepositoryInterfaces;

namespace UniversitySchedule.Core.Abstractions
{
    public interface IRepositoryManager
    {
        IUserRepository Users { get; }

        Task<int> CompleteAsync();
        int Complete();
        IDbTransaction BeginTransaction();
    }
}
