using Bookify.Domain;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Authorization
{
    public sealed class AuthorizationService
    {
        private readonly ApplicationDbContext _context;

        public AuthorizationService(ApplicationDbContext context)
        {
                _context = context;
        }

        public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
        {
            var roles = await _context.Set<User>()
                .Where(user => user.IdentityId == identityId)
                .Select(user => new UserRolesResponse
                {
                    Id = user.Id,
                    Roles = user.Roles.ToList()
                })
                .FirstAsync();

            return roles;
        }

        public async Task<HashSet<string>> GetPermissionForUserAsync(string identity)
        {
            var permissions = await _context.Set<User>()
                .Where (user => user.IdentityId == identity)
                .SelectMany(user=>user.Roles.Select(role => role.Permissions))
                .FirstAsync();

            //then convert to hashset
            var permissionSet = permissions.Select(p=>p.Name).ToHashSet();
            return permissionSet;
        }
    }

    
}

