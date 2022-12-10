using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SiteManagement.API.Filters;
using SiteManagement.API.Middlewares;
using SiteManagement.Application.Extensions;
using SiteManagement.Application.Map;
using SiteManagement.Domain.Entities;
using SiteManagement.Infrastructure.Context;
using SiteManagement.Service.Services;

namespace SiteManagement.API
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

            services.AddControllers(opt => opt.Filters.Add(new ValidateFilterAttribute()))
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.Configure<ApiBehaviorOptions>(x =>
            {
                x.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SiteManagement.API", Version = "v1" });
            });
            services.AddDbContext<AppDbContext>
               (opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(MapProfile));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;


            });


            services.Configure<ApiBehaviorOptions>(x =>
            {
                x.SuppressModelStateInvalidFilter = true;
            });
            services.DependencyExtension(Configuration);
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            // RecurringJob.AddOrUpdate<ExpenseService>(emailJob=>emailJob.SendMail(), Cron.Daily());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SiteManagement.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseExceptionMiddleware();

            app.UseAuthorization();
            app.UseHangfireDashboard("/jobs");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
