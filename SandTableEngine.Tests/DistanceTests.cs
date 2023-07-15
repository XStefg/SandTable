using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SandTableEngine.Units;

namespace SandTableEngine.Test;

[TestClass]
public class DistanceTests
{
  [TestMethod]
  public void TestImplicitCastDouble()
  {
    // Arrange/Act
    Distance angle = 12.234;

    // Assert
    angle.Value.Should().Be(12.234);

  }
  [TestMethod]
  public void TestImplicitCastDistance()
  {
    // Arrange/Act
    double doubleValue = Distance.CreateFromMeter(12.234);

    // Assert
    doubleValue.Should().Be(12.234);
  }
  [TestMethod]
  public void TestImplicitMilimeter()
  {
    // Arrange/Act
    double doubleValue = Distance.CreateFromMilimeter(12.234);

    // Assert
    doubleValue.Should().Be(0.012234);
  }
}