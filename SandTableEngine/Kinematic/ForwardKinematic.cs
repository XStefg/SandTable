using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandTableEngine.Units;

namespace SandTableEngine.Kinematic;

[DebuggerDisplay( "{CartesianCoordinates}" )]
public class ForwardKinematic
{
  public required Distance X1 { get; init; }
  public required Distance Y1 { get; init; }
  public required Distance X2 { get; init; }
  public required Distance Y2 { get; init; }

  public static ForwardKinematic ComputeFor( KinematicParameters param, Angle rotation1, Angle rotation2 )
  {
    Distance x1 = param.Distance1 * Math.Cos( rotation1 );
    Distance y1 = param.Distance1 * Math.Sin( rotation1 );
    Distance x2 = x1 + param.Distance2 * Math.Cos( rotation1 + rotation2 );
    Distance y2 = y1 + param.Distance2 * Math.Sin( rotation1 + rotation2 );
    return new() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2 };
  }

  public string CartesianCoordinates => @$"X1: {X1}, Y1: {Y1}, X2: {X2}, Y2: {Y2}";
}