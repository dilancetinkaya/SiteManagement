using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiteManagement.Application.Services;
using SiteManagement.Data.Repositories;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Context;
using SiteManagement.Infrastructure.IServices;
using SiteManagement.Infrastructure.IServices.APIServices;
using SiteManagement.Infrastructure.Repositories;
using SiteManagement.Service.Services;
using System;
using System.Reflection;

namespace SiteManagement.Application.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection DependencyExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddDbContext<AppDbContext>
              (opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IPaymentAPIService, PaymentAPIService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<IBlockRepository, BlockRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
            services.AddScoped<IFlatRepository, FlatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();



            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IBlockService, BlockService>();
            services.AddScoped<IExpenseTypeService, ExpenseTypeService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IFlatService, FlatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
           
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           

            return services;
        }
        /// <summary>
        /// Hangfire ile borclarını ödemeyenlere günlük mail atma
        /// </summary>
        /// <param name="app"></param>
        /// <param name="backgroundJobs"></param>
        /// <param name="recurringJobManager"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApplicationModule(this IApplicationBuilder app,
            IBackgroundJobClient backgroundJobs, IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

            recurringJobManager.AddOrUpdate("ExpenseMail",
                () => serviceProvider.GetService<IExpenseService>().SendMail(),
              Cron.Daily);
            return app;
        }
    }
}
