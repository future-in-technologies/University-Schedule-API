using UniversitySchedule.DAL;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using UniversitySchedule.Core.Entites;
using UniversitySchedule.DB;

namespace UniversitySchedule.API.Configurations
{
    public static class DataSeeder
    {
        private static UniversityScheduleContext _context;
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
           .GetRequiredService<IServiceScopeFactory>()
           .CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<UniversityScheduleContext>();
                _context = context;
                context.Database.Migrate();
                MigrateStaticData();
            }
        }

        private static void MigrateStaticData()
        {
            // Categories Migration From Json Static File
            //MigrateCategories();
        }

        //private static void MigrateCategories()
        //{
            //using (var reader = new StreamReader("StaticData/Categories.json"))
            //{
            //    var jsonData = reader.ReadToEnd();
            //    var categoryJsonData = JsonConvert.DeserializeObject<IEnumerable<CategoryJsonModel>>(jsonData);

            //    var dbCategories = _context.Categories.Select(category => category.SeederId).ToList();

            //    categoryJsonData = categoryJsonData.Where(x => !dbCategories.Contains(x.SeederId));

            //    if (categoryJsonData.Any())
            //    {
            //        var categories = categoryJsonData.Select(item => new Category
            //        {
            //            Name = item.Name,
            //            SeederId = item.SeederId
            //        });

            //        _context.Categories.AddRange(categories);
            //        _context.SaveChanges();
            //    }
            //}
        //}
    }
}
