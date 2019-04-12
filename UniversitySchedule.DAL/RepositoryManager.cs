using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UniversitySchedule.Core.Abstractions.RepositoryInterfaces;
using UniversitySchedule.Core.Abstractions;
using UniversitySchedule.DAL;
using UniversitySchedule.DB;

namespace UniversitySchedule.DAL
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UniversityScheduleContext _context;

        public RepositoryManager(IServiceProvider serviceProvider, UniversityScheduleContext context)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        private IUserRepository _users;
        public IUserRepository Users => _users ?? (_users = _serviceProvider.GetService<IUserRepository>());

        public IDbTransaction BeginTransaction() => new EntityDbTransaction(_context);
        public int Complete() => _context.SaveChanges();
        public Task<int> CompleteAsync() => _context.SaveChangesAsync();
    }
}
