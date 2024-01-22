using System.Drawing;
namespace TagsCloudVisualization;

public static class LayoutExamples
{
    public static void Main()
    {
        GenerateRectanglesWithRandomSizes(50, 20, 200);
        Generate200SmallSameSizedRectangles();
        GenerateVerybigThenSmallRectangles();
        Generate5000SmallSameSizedRectangles();
    }

    public static void GenerateRectanglesWithRandomSizes(int amountRectangles, int minSizeParam, int maxSizeParam)
    {
        var circularCloudLayouter = new CloudLayouter(new SpiralGenerator());
        var random = new Random();

        for (var i = 0; i < amountRectangles; i++)
        {
            var rectangleWidth = random.Next(minSizeParam, maxSizeParam);
            var rectangleHeight = random.Next(minSizeParam, maxSizeParam);
            circularCloudLayouter.PutNextRectangle(new Size(rectangleWidth, rectangleHeight));
        }

        LayoutDrawer.CreateLayoutImage(circularCloudLayouter.CreatedRectangles, "Random rectangles");
    }

    public static void Generate200SmallSameSizedRectangles()
    {
        var circularCloudLayouter = new CloudLayouter(new SpiralGenerator());

        for (var i = 0; i < 200; i++)
        {
            circularCloudLayouter.PutNextRectangle(new Size(30, 30));
        }
        LayoutDrawer.CreateLayoutImage(circularCloudLayouter.CreatedRectangles, "200 small rectangles");
    }

    public static void Generate5000SmallSameSizedRectangles()
    {
        var circularCloudLayouter = new CloudLayouter(new SpiralGenerator());

        for (var i = 0; i < 5000; i++)
        {
            circularCloudLayouter.PutNextRectangle(new Size(30, 30));
        }
        LayoutDrawer.CreateLayoutImage(circularCloudLayouter.CreatedRectangles, "5000 small rectangles");
    }

    public static void GenerateVerybigThenSmallRectangles()
    {
        var circularCloudLayouter = new CloudLayouter(new SpiralGenerator());

        for (var i = 0; i < 5; i++)
        {
            circularCloudLayouter.PutNextRectangle(new Size(150, 300));
            for (var j = 0; j < 5; j++)
                circularCloudLayouter.PutNextRectangle(new Size(20, 20));
        }
        LayoutDrawer.CreateLayoutImage(circularCloudLayouter.CreatedRectangles, "Small rectangles after very big");
    }
}