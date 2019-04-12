using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using UniversitySchedule.API.ConfigurationExtensions;
using UniversitySchedule.API.Configurations;
using UniversitySchedule.API.ErrorHandling;
using UniversitySchedule.BLL.Operations;
using UniversitySchedule.Core.Abstractions.OperationInterfaces;
using UniversitySchedule.Core.Models;
using UniversitySchedule.DB;
using UniversitySchedule.DI;

namespace UniversitySchedule.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<UniversityScheduleContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"], m => m.MigrationsAssembly("UniversitySchedule.DB"));
                options.EnableSensitiveDataLogging(true);
            });

            services.AddAutoMapper();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "University Schedule API" });
            });
            #region JsonSerializer Configuration
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new DateTimeConverter());
            var serializer = JsonSerializer.Create(serializerSettings);
            #endregion
            #region Dependancy Injections
            services.AddTransient(x => serializer);
            services.AddRepositoriesAndBussinesLayerServices();
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var appSettingsSection = Configuration.GetSection("TokenAuthentification");
            services.Configure<TokenAuthentification>(appSettingsSection);

            var appSettings = appSettingsSection.Get<TokenAuthentification>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserOperations>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IUserOperations, UserOperations>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
            });
            app.UseStaticFiles();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.

            app.UseWebSockets();
            app.UseMiddleware<WebSocketMiddleware>();

            app.UseAuthentication();

            //app.UseSwaggerCustomUI(provider);
            //OR
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "API V1");
            });


            //app.UseSignalR(options =>
            //{
            //    options.MapHub<LiveUpdatesHub>("liveUpdates");
            //});
            app.SeedData();

            app.UseMvc();
        }
    }
}
