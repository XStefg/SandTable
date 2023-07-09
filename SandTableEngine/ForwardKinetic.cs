using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandTableEngine;

public class ForwardKinetic
{
  private ForwardKinetic( Distance x1, Distance y1, Distance x2, Distance y2 )
  {
    X1 = x1;
    Y1 = y1;
    X2 = x2;
    Y2 = y2;
  }

  public Distance X1 { get; }
  public Distance Y1 { get; }
  public Distance X2 { get; }
  public Distance Y2 { get; }

  public static ForwardKinetic ComputeFor( KineticParameters param, Angle rotation1, Angle rotation2 )
  {
    Distance x1 = param.Distance1 * Math.Cos( rotation1 );
    Distance y1 = param.Distance1 * Math.Sin( rotation1 );
    Distance x2 = x1 + param.Distance2 * Math.Cos( rotation1 + rotation2 );
    Distance y2 = y1 + param.Distance2 * Math.Sin( rotation1 + rotation2 );
    return new ForwardKinetic( x1, y1, x2, y2 );
  }
}