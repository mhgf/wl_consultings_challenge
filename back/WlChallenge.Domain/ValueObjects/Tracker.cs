namespace WlChallenge.Domain.ValueObjects;

public record Tracker
{
    #region Constructors

    private Tracker()
    {
    }

    private Tracker(DateTime createdAtUtc, DateTime updatedAtUtc)
    {
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
    }

    #endregion

    #region Factories

    public static Tracker Create()
        => new(DateTime.UtcNow, DateTime.UtcNow);

    #endregion

    #region Properties

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }

    #endregion

    #region Public Methods

    public void Update()
    {
        UpdatedAtUtc = DateTime.UtcNow;
    }

    #endregion
}