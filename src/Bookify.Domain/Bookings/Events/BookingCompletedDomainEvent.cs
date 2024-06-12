namespace Bookify.Domain;

public record BookingCompletedDomainEvent(Guid BookingId) : IDomainEvent;
