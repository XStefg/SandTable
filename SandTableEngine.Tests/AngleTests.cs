using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandTableEngine.Tests;

[TestClass]
public class AngleTests 
{
  [TestMethod]
  public void TestImplicitCastDouble()
  {
    // Arrange/Act
    Angle angle = Math.PI;

    // Assert
    angle.Value.Should().Be(Math.PI);
    angle.ValueInDegree.Should().Be(180.0);

  }
  [TestMethod]
  public void TestImplicitCastAngle()
  {
    // Arrange/Act
    double doubleValue = Angle.CreateFromRad(Math.PI);

    // Assert
    doubleValue.Should().Be(Math.PI);
  }

  [TestMethod]
  public void TestDegree()
  {
    // Arrange/Act
    Angle angle = Angle.CreateFromDegree(180.0);

    // Assert
    angle.Value.Should().Be(Math.PI);
    angle.ValueInDegree.Should().Be(180.0);
  }

  [TestMethod]
  [DynamicData( nameof(AdditionData) )]
  public void TestCast(Angle angle, double expectedAngle)
  {
    angle.Value.Should().Be(expectedAngle);  
  }
  public static IEnumerable<object[]> AdditionData =>
    new[]
    {
      new object[] { Angle.CreateFromDegree( 10.0 ), Math.PI * 10.0 / 180.0 },
      new object[] { Angle.CreateFromDegree( 20.0 ), Math.PI * 20.0 / 180.0 },
    };

}