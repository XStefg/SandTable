using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandTableEngine;

public class ForwardKinetic
{
  private ForwardKinetic( Distance x, Distance y )
  {
    X = x;
    Y = y;
  }

  public Distance X { get; }
  public Distance Y { get; }

  public static ForwardKinetic ComputeFor( KineticParameters param, Angle rotation1, Angle rotation2 )
  {
    Distance x = param.Distance1 * Math.Cos( rotation1 ) + param.Distance2 * Math.Cos( rotation1 + rotation2 );
    Distance y = param.Distance1 * Math.Sin( rotation1 ) + param.Distance2 * Math.Sin( rotation1 + rotation2 );
    return new ForwardKinetic( x, y );
  }
}