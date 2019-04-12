using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using UniversitySchedule.Core.Abstractions;

namespace UniversitySchedule.DAL
{
    public class EntityDbTransaction : IDbTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public EntityDbTransaction(DbContext context)
        {
            _transaction = context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transaction.Dispose();
            }
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
