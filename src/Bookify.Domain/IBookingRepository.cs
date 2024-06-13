namespace Bookify.Domain;

public interface IBookingRepository
{
    Task<bool> IsOverlappingAsync(Apartment apartment, DateRange range, CancellationToken cancellationToken);

    void Add(Booking booking);
}
