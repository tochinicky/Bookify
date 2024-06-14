namespace Bookify.Domain;

public interface IBookingRepository
{
    Task<bool> IsOverlappingAsync(Apartment apartment, DateRange range, CancellationToken cancellationToken);
    Task<Booking?> GetByIdAsync(Guid bookingId, CancellationToken cancellationToken);

    void Add(Booking booking);
}
