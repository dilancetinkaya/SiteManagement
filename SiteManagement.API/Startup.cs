using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SiteManagement.API.Middlewares;
using SiteManagement.Application.Map;
using SiteManagement.Data.Repositories;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Context;
using SiteManagement.Infrastructure.IServices;
using SiteManagement.Infrastructure.Repositories;
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

            services.AddControllers();
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
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<IBlockRepository, BlockRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
            services.AddScoped<IFlatRepository, FlatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

           
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IBlockService, BlockService>();
            services.AddScoped<IExpenseTypeService, ExpenseTypeService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IFlatService, FlatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
