using System;
using System.Collections.Generic;
using System.Text;
using UniversitySchedule.Core.Abstractions.RepositoryInterfaces;
using UniversitySchedule.Core.Entites;
using UniversitySchedule.DB;

namespace UniversitySchedule.DAL.Repositories
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(UniversityScheduleContext context) : base(context)
        {
        }
    }
}
