namespace Bookify.Domain;

public sealed record BookingReservedDomainEvent(Guid BookingId) : IDomainEvent;
