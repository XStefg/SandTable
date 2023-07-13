namespace SandTableEngine;

public class KinematicParameters
{
  public KinematicParameters( Distance distance1, Distance distance2 )
  {
    Distance1 = distance1;
    Distance2 = distance2;
  }

  public Distance Distance1 { get; }
  public Distance Distance2 { get; }

}