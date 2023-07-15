using System.Diagnostics;
using Microsoft.VisualBasic.CompilerServices;
using SandTableEngine.Units;

namespace SandTableEngine.File;

[DebuggerDisplay( "{Angle}, {Radius}" )]
public struct ThetaRadiusPoint

{
  public Angle    Angle  { get; init; }
  public Distance Radius { get; init; }

  public static ThetaRadiusPoint Empty { get; } = new ThetaRadiusPoint { Radius = -1.0 };

  public bool IsEmpty => Radius < 0.0;

  #region Operators

  public static ThetaRadiusPoint operator -( ThetaRadiusPoint a, ThetaRadiusPoint b )
    => new ThetaRadiusPoint{ Angle = a.Angle - b.Angle, Radius = a.Radius - b.Radius };

  #endregion
}