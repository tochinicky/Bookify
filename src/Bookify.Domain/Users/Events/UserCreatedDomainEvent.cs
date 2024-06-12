namespace Bookify.Domain;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
