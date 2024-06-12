namespace Bookify.Domain;

public sealed record BookingConfirmedDomainEvent(Guid BookingId) : IDomainEvent;

