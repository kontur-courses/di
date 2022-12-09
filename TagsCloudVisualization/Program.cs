using System.Drawing;

namespace TagsCloudVisualization;

public class Program
{
    public static void Main(string[] args)
    {
        var cloudGenerator = GetCloudGenerator();
        var generatedCloud = cloudGenerator.GetGeneratedCloud();

        var pen = new Pen(Color.Black, 2);
        var layoutDrawer = new LayoutDrawer(pen);

        //layoutDrawer.Draw(generatedCloud, "Ex122255.png");
    }

    private static CloudGenerator GetCloudGenerator()
    {
        var minSize = new Size(50, 50);
        var maxSize = new Size(200, 200);
        var center = new Point(0, 0);

        ICurve archSpiral = new ArchimedeanSpiral(1, 1, 0);
        ILayouter layouter = new CircularCloudLayouter(center, archSpiral);

        return new CloudGenerator(120, minSize, maxSize, layouter);
    }
}