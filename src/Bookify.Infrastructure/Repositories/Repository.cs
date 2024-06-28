using Bookify.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure;

internal abstract class Repository<T>
    where T : Entity
{
    protected readonly ApplicationDbContext DbContext;
    public Repository(ApplicationDbContext context)
    {
        DbContext = context;
    }

    public async Task<T?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext
        .Set<T>()
        .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public virtual void Add(T entity)
    {
        DbContext.Add(entity);
    }
}
