using NUnit.Framework;
using TagsCloudContainer;

namespace TagCloudContainerTests
{
    public class ScaleCoefficientCalculatorTests
    {
        [TestCase(1000, 1000, 100, 8, TestName = "Standart values")]
        [TestCase(500, 500, 0, 6, TestName = "Without borders")]
        [TestCase(0, 0, 0, 0, TestName = "Zero values")]
        public void CalculateScaleCoefficient_ShouldReturnCorrectCoefficient(int canvasWidth,
            int canvasHeight,
            int canvasBorder,
            int expectedResult)
        {
            ScaleCoefficientCalculator.CalculateScaleCoefficient(canvasWidth, canvasHeight, canvasBorder);
        }
    }
}