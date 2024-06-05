using Ardalis.Specification;

namespace ClientApp.Shared.Interfaces;

public interface IReadRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
