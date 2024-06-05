using Ardalis.Specification.EntityFrameworkCore;
using ClientApp.Shared.Interfaces;

namespace ClientApp.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}
