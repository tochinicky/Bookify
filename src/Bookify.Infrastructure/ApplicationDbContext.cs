using Bookify.Application;
using Bookify.Domain;
using Bookify.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Bookify.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private static readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
    };
    private readonly IDateTimeProvider _dateTimeProvider;
    public ApplicationDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    //publishing domain events using EF
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            AddDomainEventsAsOutboxMessages();
            var result = await base.SaveChangesAsync(cancellationToken);

          
            return result;
        }
        catch (DbUpdateConcurrencyException exception)
        {

            throw new ConcurrencyException("Concurrency exception occurred", exception);
        }
    }

    private void AddDomainEventsAsOutboxMessages()
    {
        var outBoxMessages = ChangeTracker
        .Entries<Entity>()
        .Select(entry => entry.Entity)
        .SelectMany(entity =>
        {
            var domainEvents = entity.GetDomainEvents();
            entity.ClearDomainEvents();
            return domainEvents;
        }).Select(domainEvent=> new OutboxMessage(Guid.NewGuid(),_dateTimeProvider.UtcNow,domainEvent.GetType().Name,JsonConvert.SerializeObject(domainEvent, jsonSerializerSettings)))
        .ToList();
        //Add them to the change tracker
        AddRange(outBoxMessages);

    }
}
