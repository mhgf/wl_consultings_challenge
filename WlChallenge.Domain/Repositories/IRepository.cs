using WlChallenge.Domain.Entities;
using WlChallenge.Domain.Entities.Abstractions;

namespace WlChallenge.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : IAggregateRoot;