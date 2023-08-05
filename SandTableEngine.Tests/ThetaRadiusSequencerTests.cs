using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandTableEngine.File;
using SandTableEngine.Processor;
using SandTableEngine.Processor.ThetaRadius;
using SandTableEngine.Units;

namespace SandTableEngine.Tests
{
  [TestClass]
  public class ThetaRadiusSequencerTests
  {
    #region Initialization and Cleanup

    [TestInitialize]
    public void Initialize()
    {
    }

    [TestCleanup]
    public void Cleanup()
    {
    }

    #endregion

    #region Test Methods

    [TestMethod]
    public void Process_FullTurn()
    {
      // Arrange
      ThetaRadiusPoint[] points =
      {
        new() { Radius = 0.0, Angle = 0.0, }, 
        new() { Radius = 1.0, Angle = Angle.CreateFromDegree( 360 ) },
      };

      ProcessingBuffer<ThetaRadiusPoint> inputBuffer = new() { Buffer = points };

      ThetaRadiusSequencerConfig config = new ThetaRadiusSequencerConfig() { MinimumDistance = .050 };

      ThetaRadiusSequencer sequencer = new( config );

      // Act
      sequencer.Process( inputBuffer );
      ProcessingBuffer<ThetaRadiusPoint> outputBuffer = sequencer.GetOutput();

      // Assert
      outputBuffer.Buffer.Length.Should().Be( 92 );
      outputBuffer.Buffer[0].Should().Be( points[0] );
      outputBuffer.Buffer.Last().Should().Be( points.Last() );

      Distance[] deltaPoints = outputBuffer.Buffer
                                           .Zip( outputBuffer.Buffer.Skip( 1 ),
                                                 ( a, b ) => ThetaRadiusSequenceCalculator.GetDistanceBetweenPoints( a, b ) )
                                           .ToArray();

      deltaPoints.Where( p => p > config.MinimumDistance ).Should().BeEmpty();
    }

    #endregion
  }
}