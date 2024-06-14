namespace Bookify.Api;

public record ReserveBookingRequest(Guid ApartmentId,
Guid UserId, DateOnly StartDate, DateOnly EndDate);

