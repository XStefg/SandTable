namespace SandTableEngine
{
  public readonly struct Angle
  {
    #region CTOR

    /// <summary>
    /// CTOR
    /// </summary>
    /// <param name="value">value in rad</param>
    private Angle(double value)
    {
      Value = value;
    }

    #endregion

    #region Public Properties

    public double Value { get; }

    public double ValueInDegree => Value * 180 / Math.PI;

    #endregion

    #region public Methods

    public static Angle CreateFromDegree(double value) => new(value * Math.PI / 180);

    public static Angle CreateFromRad(double value) => new(value * 180 / Math.PI);

    #endregion

    #region Operators

    public static implicit operator Angle(double value) => new(value);
    public static explicit operator double(Angle angle) => angle.Value;

    #endregion
  }
}