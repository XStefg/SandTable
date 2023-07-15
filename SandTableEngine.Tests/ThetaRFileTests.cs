using System;
using System.Collections.Generic;
using System.Linq;
using Eddyfi.Core.Test.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandTableEngine.Tests
{
  [TestClass]
  public class ThetaRFileTests
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
    public void CreateFromFile_ProperFile()
    {
      // Arrange
      string file = m_Context.GetPathFor( "Hepreverse.thr" );

      // Act
      ThetaRFile result = ThetaRFile.CreateFromFile( file );

      // Assert
      result.Should().NotBeNull();
      result.Points.Count.Should().Be( 2047 );
      result.Points[ 0 ].Angle.Should().Be( ( Angle )0.0 );
      result.Points[ 0 ].Radius.Should().Be( ( Distance )0.0 );
      result.Points[ 1 ].Angle.Should().Be( ( Angle )319.257396 );
      result.Points[ 1 ].Radius.Should().Be( ( Distance )1.0 );
    }

    #endregion

    TestFileDirectoryContext m_Context = new TestFileDirectoryContext( typeof( ThetaRFileTests ) );

  }
}
