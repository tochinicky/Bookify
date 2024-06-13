namespace Bookify.Application;

public record ReservedBookingCommand(
    Guid ApartmentId,
Guid UserId, DateOnly StartDate,
DateOnly EndDate) : ICommand<Guid>;
