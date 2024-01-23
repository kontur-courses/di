using TagsCloudVisualization.PointsProviders;

namespace TagsCloudVisualizationTests;

public class ArchimedianSpiralPointsProviderTests
{
     [TestCase(0, 2, TestName = "spiralDeltaAngle is zero")]
     [TestCase(5 * Math.PI / 180, 0, TestName = "spiralDistance is zero")]
     public void Constructor_ThrowsException_OnIncorrectArguments(double spiralDeltaAngle, double spiralDistance)
     {
         var a = () => 
             new ArchimedeanSpiralPointsProvider(new ArchimedeanSpiralSettings {Center = Point.Empty,
                 DeltaAngle = spiralDeltaAngle, 
                 Distance = spiralDistance});
         
         a.Should().Throw<ArgumentException>();
     }

     [TestCase(0, 0, 0, 0)]
     [TestCase(1, 0, 1, 0)]
     [TestCase(1, Math.PI / 2, 0, 1)]
     [TestCase(1, Math.PI, -1, 0)]
     [TestCase(10, Math.PI / 4, 7, 7)]
     [TestCase(10, 5 * Math.PI / 4, -7, -7)]
     public void PolarToCartesian_TranslatesCoordinatesCorrectly(double distance, double angle, int expectedX, int expectedY)
     {
         var actual = ArchimedeanSpiralPointsProvider.PolarToCartesian(distance, angle);
         var expected = new Point(expectedX, expectedY);

         actual.Should().BeEquivalentTo(expected);
     }
}