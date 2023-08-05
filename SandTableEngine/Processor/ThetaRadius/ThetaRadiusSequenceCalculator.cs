using System;
using System.Collections.Generic;
using SandTableEngine.File;
using SandTableEngine.Units;

namespace SandTableEngine.Processor.ThetaRadius;

public static class ThetaRadiusSequenceCalculator
{
  public static Distance GetDistanceBetweenPoints( ThetaRadiusPoint a, ThetaRadiusPoint b )
  {
    return Math.Sqrt( a.Radius * a.Radius + b.Radius * b.Radius - 2.0 * a.Radius * b.Radius * Math.Cos( a.Angle - b.Angle ) );
  }
}