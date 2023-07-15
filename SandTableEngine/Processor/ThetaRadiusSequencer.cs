using System;
using System.Collections.Generic;
using SandTableEngine.File;
using SandTableEngine.Units;

namespace SandTableEngine.Processor;

public class ThetaRadiusSequencer : ProcessorBase<ThetaRadiusPoint, ThetaRadiusPoint, ThetaRadiusSequencerConfig>
{
  #region CTOR

  public ThetaRadiusSequencer( ThetaRadiusSequencerConfig config ) : base( config )
  {
  }

  #endregion

  #region Base class overrides

  public override bool Process( ProcessingBuffer<ThetaRadiusPoint> input )
  {
    if ( input.Buffer.Length == 0 )
    {
      m_OutputBuffer = ProcessingBuffer<ThetaRadiusPoint>.Empty;
    }

    List<ThetaRadiusPoint> output = new();

    int index = 0;

    if ( m_LastPoint.IsEmpty )
    {
      output.Add( input.Buffer[0] );
      m_LastPoint = input.Buffer[0];
      index++;
    }

    while ( index < input.Buffer.Length )
    {
      ThetaRadiusPoint currentPoint = input.Buffer[index];

      ThetaRadiusPoint delta = input.Buffer[index] - m_LastPoint;

      Angle minAngleAtRadius = GetMinAngleAtRadius( currentPoint.Radius );

      if ( Math.Abs( delta.Radius ) < Config.MinimumDistance && Math.Abs( delta.Angle) < minAngleAtRadius )
      {
        output.Add( currentPoint );
        index++;
      }
      else
      {
        ThetaRadiusPoint intermediatePoint
          = new()
            {
              Angle  = m_LastPoint.Angle  + Math.Sign( delta.Angle )  * minAngleAtRadius,
              Radius = m_LastPoint.Radius + Math.Sign( delta.Radius ) * Config.MinimumDistance
            };
        output.Add( intermediatePoint );
        m_LastPoint = intermediatePoint;
      }
    }

    m_OutputBuffer = new ProcessingBuffer<ThetaRadiusPoint> { Buffer = output.ToArray() };

    return true;
  }

  public override ProcessingBuffer<ThetaRadiusPoint> GetOutput() => m_OutputBuffer;

  #endregion

  #region Public Methods

  public Angle GetMinAngleAtRadius( Distance radius )
  {
    return Math.Acos( 1.0 - Config.MinimumDistance * Config.MinimumDistance / ( 2.0 * radius ) );
  }

  #endregion

  #region Private Variables

  private ProcessingBuffer<ThetaRadiusPoint> m_OutputBuffer = ProcessingBuffer<ThetaRadiusPoint>.Empty;

  private ThetaRadiusPoint m_LastPoint = ThetaRadiusPoint.Empty;

  #endregion
}