using System;
using System.Collections.Generic;
using System.Linq;
using SandTableEngine.File;
using SandTableEngine.Units;

namespace SandTableEngine.Processor.ThetaRadius;

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
    List<ThetaRadiusPoint> output = new( input.Buffer );

    if ( input.Buffer.Length < 2 )
    {
      m_OutputBuffer = new ProcessingBuffer<ThetaRadiusPoint> { Buffer = output.ToArray() };
    }

    int index = 0;

    if ( !m_LastPoint.IsEmpty )
    {
      output.Insert( 0, m_LastPoint );
    }

    while ( index < output.Count - 1 )
    {
      ThetaRadiusPoint currentPoint      = output[index];
      ThetaRadiusPoint nextPoint         = output[index + 1];
      ThetaRadiusPoint intermediatePoint = ( currentPoint + nextPoint ) / 2.0;

      Distance distanceBetweenPoints = ThetaRadiusSequenceCalculator.GetDistanceBetweenPoints( currentPoint, nextPoint );

      if ( distanceBetweenPoints < Config.MinimumDistance )
      {
        index++;
      }
      else
      {
        output.Insert( index + 1, intermediatePoint );
      }
    }

    m_LastPoint = input.Buffer.Last();

    m_OutputBuffer = new ProcessingBuffer<ThetaRadiusPoint> { Buffer = output.ToArray() };

    return true;
  }

  public override ProcessingBuffer<ThetaRadiusPoint> GetOutput() => m_OutputBuffer;

  #endregion

  #region Public Methods

  #endregion

  #region Private Variables

  private ProcessingBuffer<ThetaRadiusPoint> m_OutputBuffer = ProcessingBuffer<ThetaRadiusPoint>.Empty;

  private ThetaRadiusPoint m_LastPoint = ThetaRadiusPoint.Empty;

  #endregion
}