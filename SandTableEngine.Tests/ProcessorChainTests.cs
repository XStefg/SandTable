using Eddyfi.Core.Test.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandTableEngine.File;
using SandTableEngine.Processor.FileReader;
using SandTableEngine.Processor.ThetaRadius;
using SandTableEngine.Tests;
using SandTableEngine.Units;

namespace SandTableEngine.Test;

[TestClass]
public class ProcessorChainTests
{
  [TestMethod]
  public void FileProcessorChainTest()
  {
    // Arrange
    string file = m_Context.GetPathFor( "Spiral7.thr" );

    ThetaRadiusFileReaderConfig fileReaderConfig = new ThetaRadiusFileReaderConfig { FileName = file };
    ThetaRadiusFileReader       fileReader       = new ThetaRadiusFileReader( fileReaderConfig );

    ThetaRadiusSequencerConfig sequencerConfig = new ThetaRadiusSequencerConfig { MinimumDistance = 0.05 };
    ThetaRadiusSequencer       sequencer       = new ThetaRadiusSequencer( sequencerConfig );

    // Connect Sequence together
    sequencer.Input = fileReader;

    // Act
    ThetaRadiusPoint[] resultingPoints = sequencer.ProcessSamples().ToArray();

    // Assert
    Distance[] deltaResultingPoints = resultingPoints
                                      .Zip( resultingPoints.Skip( 1 ),
                                            ThetaRadiusSequenceCalculator.GetDistanceBetweenPoints )
                                      .ToArray();
    deltaResultingPoints.Where( p => p > sequencerConfig.MinimumDistance ).Should().BeEmpty();
  }

  TestFileDirectoryContext m_Context = new TestFileDirectoryContext( typeof( ProcessorChainTests ) );
}