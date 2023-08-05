using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SandTableEngine.File;
using SandTableEngine.Units;

namespace SandTableEngine.Processor.ThetaRadius;

public class ThetaRadiusSequencer : ProcessorBase<ThetaRadiusSequencerConfig>, IProcessInput<ThetaRadiusPoint>, IProcessOutput<ThetaRadiusPoint>
{
  #region CTOR

  public ThetaRadiusSequencer( ThetaRadiusSequencerConfig config ) : base( config )
  {
  }

  #endregion

  public IProcessOutput<ThetaRadiusPoint>? Input { get; set; }

  public IEnumerable<ThetaRadiusPoint> ProcessSamples()
  {
    if ( Input == null )
    {
      yield break;
    }

    bool             firstPoint    = true;
    ThetaRadiusPoint previousPoint = ThetaRadiusPoint.Empty;

    foreach ( ThetaRadiusPoint currentPoint in Input.ProcessSamples() )
    {
      if ( firstPoint )
      {
        firstPoint    = false;
        previousPoint = currentPoint;
        yield return currentPoint;
        continue;
      }

      foreach ( ThetaRadiusPoint betweenPoint in GetPointBetween( previousPoint, currentPoint ) )
      {
        yield return betweenPoint;
      }

      yield return currentPoint;

      previousPoint = currentPoint;
    }
  }

  private IEnumerable<ThetaRadiusPoint> GetPointBetween( ThetaRadiusPoint previousPoint, ThetaRadiusPoint nextPoint )
  {
    Distance distanceBetweenPoints = ThetaRadiusSequenceCalculator.GetDistanceBetweenPoints( previousPoint, nextPoint );

    if ( distanceBetweenPoints < Config.MinimumDistance )
    {
      yield break;
    }
    else
    {
      ThetaRadiusPoint intermediatePoint = ( nextPoint + previousPoint ) / 2.0;
      foreach ( ThetaRadiusPoint currentPoint in GetPointBetween( previousPoint, intermediatePoint ) )
      {
        yield return currentPoint;
      }

      yield return intermediatePoint;

      foreach ( ThetaRadiusPoint currentPoint in GetPointBetween( intermediatePoint, nextPoint ) )
      {
        yield return currentPoint;
      }
    }
  }
}