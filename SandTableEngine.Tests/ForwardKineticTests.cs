using FluentAssertions;

namespace SandTableEngine.Test;

[TestClass]
public class ForwardKineticTests
{
  [TestMethod]
  [DynamicData( nameof(AdditionData) )]
  public void TestMethod1( Angle angle1, Angle angle2, Distance x1Position, Distance y1Position, Distance x2Position, Distance y2Position )
  {
    // Arrange
    KineticParameters parameters = CreateParameters();

    // Act
    ForwardKinetic kinetic = ForwardKinetic.ComputeFor( parameters, angle1, angle2 );

    // Assert
    kinetic.X1.Value.Should().BeApproximately( x1Position, 0.001 );
    kinetic.Y1.Value.Should().BeApproximately( y1Position, 0.001 );
    kinetic.X2.Value.Should().BeApproximately( x2Position, 0.001 );
    kinetic.Y2.Value.Should().BeApproximately( y2Position, 0.001 );
  }

  public static IEnumerable<object[]> AdditionData =>
    new[]
    {
      Create(   0.0,   0.0,  2.0,  0.0,  3.0,  0.0 ), 
      Create(  90.0,   0.0,  0.0,  2.0,  0.0,  3.0 ), 
      Create(   0.0,  90.0,  2.0,  0.0,  2.0,  1.0 ),
      Create( 180.0,  90.0, -2.0,  0.0, -2.0, -1.0 ), 
      Create(  90.0, -90.0,  0.0,  2.0,  1.0,  2.0 ), 
      Create( -90.0,  90.0,  0.0, -2.0,  1.0, -2.0 ), 
      Create( -90.0,   0.0,  0.0, -2.0,  0.0, -3.0 ),
    };

  KineticParameters CreateParameters() => new KineticParameters( 2.0, 1.0 );

  private static object[] Create( double angle1, double angle2, Distance x1Position, Distance y1Position, Distance x2Position, Distance y2Position )
    => new object[] { Angle.CreateFromDegree( angle1 ), Angle.CreateFromDegree( angle2 ), x1Position, y1Position, x2Position, y2Position };
}