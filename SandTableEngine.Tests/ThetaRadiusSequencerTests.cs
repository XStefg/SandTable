using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandTableEngine.File;
using SandTableEngine.Processor;
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
      // This method is executed before each individual test method.
    }

    [TestCleanup]
    public void Cleanup()
    {
      // This method is executed after each individual test method.
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

      ThetaRadiusSequencerConfig config = new ThetaRadiusSequencerConfig() { MinimumDistance = .005 };

      ThetaRadiusSequencer sequencer = new( config );

      // Act
      sequencer.Process( inputBuffer );
      ProcessingBuffer<ThetaRadiusPoint> outputBuffer = sequencer.GetOutput();

      // Assert
      outputBuffer.Buffer.Length.Should().Be( 201 );
      outputBuffer.Buffer[0].Should().Be( points[0] );
      outputBuffer.Buffer.Last().Should().Be( points.Last() );

      ThetaRadiusPoint[] deltaPoints = outputBuffer.Buffer
        .Zip( outputBuffer.Buffer.Skip( 1 ), ( a, b ) => new ThetaRadiusPoint() { Radius = b.Radius - a.Radius, Angle = b.Angle - a.Angle } )
        .ToArray();

      deltaPoints.All( p => p.Radius <= config.MinimumDistance ).Should().BeTrue();
      deltaPoints.All( p => p.Angle <= sequencer.GetMinAngleAtRadius( p.Radius) ).Should().BeTrue();
    }

    #endregion
  }
}