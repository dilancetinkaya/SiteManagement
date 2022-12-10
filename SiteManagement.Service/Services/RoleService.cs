using Microsoft.AspNetCore.Identity;
using SiteManagement.Domain.Entities;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task AddRole(string name )
        {
           await _roleManager.CreateAsync(new Role { Name = name });
          
        }
    }
}
