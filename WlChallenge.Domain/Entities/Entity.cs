using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Domain.Entities;

public abstract class Entity : IEquatable<Guid>
{
    #region Properties

    public Guid Id { get; } = Guid.CreateVersion7();
    public Tracker Tracker { get; private set; } = Tracker.Create();

    #endregion


    #region Equatable Implementations

    public bool Equals(Guid other) => Id == other;

    public override int GetHashCode() => Id.GetHashCode();

    #endregion
}