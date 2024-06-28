using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Users
{
    public sealed class Role
    {
        public static readonly Role Registered = new (1, "Registered");
        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;

        public ICollection<User> Users { get; init; } = new List<User>();// createed a navigation property to point to the user, however this is to manage the roles in the domain

        public ICollection<Permission> Permissions { get; init; } = new List<Permission>();
    }
}
