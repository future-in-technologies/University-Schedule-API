using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using UniversitySchedule.BLL.Operations;
using UniversitySchedule.Core.Abstractions;
using UniversitySchedule.Core.Abstractions.OperationInterfaces;
using UniversitySchedule.Core.Abstractions.RepositoryInterfaces;
using UniversitySchedule.DAL;
using UniversitySchedule.DAL.Repositories;

namespace UniversitySchedule.DI
{
    public static class DependancyConfiguration
    {
        public static void AddRepositoriesAndBussinesLayerServices(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddBussinesLayerServices();
        }
        public static void AddRepositories(this IServiceCollection services)
        {

            foreach (var entry in RepositoryServiceAndImplementationTypes)
            {

                services.Add(new ServiceDescriptor(entry.Key, entry.Value, ServiceLifetime.Transient));
            }
        }

        public static void AddBussinesLayerServices(this IServiceCollection services)
        {

            foreach (var entry in BussinesLayerServiceAndImplementationTypes)
            {

                services.Add(new ServiceDescriptor(entry.Key, entry.Value, ServiceLifetime.Transient));
            }
        }
        private static readonly Dictionary<Type, Type> RepositoryServiceAndImplementationTypes = new Dictionary<Type, Type>
        {
            {typeof(IRepositoryManager), typeof(RepositoryManager)},

            { typeof(IUserRepository), typeof(UserRepository)},
        };

        private static readonly Dictionary<Type, Type> BussinesLayerServiceAndImplementationTypes = new Dictionary<Type, Type>
        {
            {typeof(IUserOperations),typeof(UserOperations)},
        };
    }
}
