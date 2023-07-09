using FluentAssertions;

namespace SandTableEngine.Test;

[TestClass]
public class ForwardKineticTests
{
  [TestMethod]
  [DynamicData( nameof(AdditionData) )]
  public void TestMethod1( Angle angle1, Angle angle2, Distance xPosition, Distance yPosition )
  {
    // Arrange
    KineticParameters parameters = CreateParameters();

    // Act
    ForwardKinetic kinetic = ForwardKinetic.ComputeFor( parameters, angle1, angle2 );

    // Aert
    kinetic.Y.Value.Should().BeApproximately( yPosition, 0.001 );
    kinetic.X.Value.Should().BeApproximately( xPosition, 0.001 );
  }

  public static IEnumerable<object[]> AdditionData =>
    new[] { 
            new object[] { Angle.CreateFromDegree( 180.0 ),  Angle.CreateFromDegree( 90.0 ), Distance.CreateFromMeter( -2.0 ), Distance.CreateFromMeter( -1.0 ) },
            new object[] { Angle.CreateFromDegree( 90.0 ),  Angle.CreateFromDegree( -90.0 ), Distance.CreateFromMeter( 1.0 ), Distance.CreateFromMeter( 2.0 ) },
            new object[] { Angle.CreateFromDegree( -90.0 ),  Angle.CreateFromDegree( 90.0 ), Distance.CreateFromMeter( 1.0 ), Distance.CreateFromMeter( -2.0 ) },
            new object[] { Angle.CreateFromDegree( -90.0 ),  Angle.CreateFromDegree( 0.0 ), Distance.CreateFromMeter( 0.0 ), Distance.CreateFromMeter( -3.0 ) },
            new object[] { Angle.CreateFromDegree(  0.0  ),  Angle.CreateFromDegree( 0.0 ), Distance.CreateFromMeter( 3.0 ), Distance.CreateFromMeter( 0.0 ) },
            new object[] { Angle.CreateFromDegree(  90.0 ),  Angle.CreateFromDegree( 0.0 ), Distance.CreateFromMeter( 0.0 ), Distance.CreateFromMeter( 3.0 ) },
            new object[] { Angle.CreateFromDegree(  0.0  ),  Angle.CreateFromDegree( 90.0 ), Distance.CreateFromMeter( 2.0 ), Distance.CreateFromMeter( 1.0 ) },
          };

  KineticParameters CreateParameters() => new KineticParameters( 2.0, 1.0 );
}