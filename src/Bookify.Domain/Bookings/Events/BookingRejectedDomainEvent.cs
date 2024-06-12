namespace Bookify.Domain;

public sealed record BookingRejectedDomainEvent(Guid BookingId) : IDomainEvent;
