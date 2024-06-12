namespace Bookify.Domain;

public sealed record BookingCancelledDomainEvent(Guid BookingId) : IDomainEvent;
