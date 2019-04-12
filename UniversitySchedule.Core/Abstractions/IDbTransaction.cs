using System;
using System.Collections.Generic;
using System.Text;

namespace UniversitySchedule.Core.Abstractions
{
    public interface IDbTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
