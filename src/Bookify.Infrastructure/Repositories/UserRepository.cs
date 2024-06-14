﻿using Bookify.Domain;

namespace Bookify.Infrastructure;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}
