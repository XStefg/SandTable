using System.Diagnostics;

namespace SandTableEngine;

[DebuggerDisplay("{Angle}, {Radius}")] 
public struct ThetaRPoint
{
  public Angle    Angle { get; init; }
  public Distance Radius     { get; init; }
}