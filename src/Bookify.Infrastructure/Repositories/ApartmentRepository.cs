﻿using Bookify.Domain;

namespace Bookify.Infrastructure;

internal sealed class ApartmentRepository : Repository<Apartment>, IApartmentRepository
{
    public ApartmentRepository(ApplicationDbContext context) : base(context)
    {
    }

}
