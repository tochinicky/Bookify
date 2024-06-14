﻿using Bookify.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure;

internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
{
    private static readonly BookingStatus[] ActiveBookingStatuses =
    {
        BookingStatus.Reserved,
        BookingStatus.Confirmed,
        BookingStatus.Completed
    };
    public BookingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken)
    {
        return await _context
        .Set<Booking>()
        .AnyAsync(booking => booking.ApartmentId == apartment.Id &&
        booking.Duration.Start <= duration.End &&
        booking.Duration.End >= duration.Start &&
        ActiveBookingStatuses.Contains(booking.Status), cancellationToken);
    }
}
