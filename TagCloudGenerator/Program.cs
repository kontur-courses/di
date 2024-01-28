using CommandLine;
using System.Drawing;
using TagCloudGenerator;
using TagsCloudVisualization;
using TagsCloudVisualization.PointDistributors;

public class Program
{
    static TagCloudDrawer tagCloudDrawer = new TagCloudDrawer();
    
    public static void Main(string[] args)
    {
        var visualizingSettings = new VisualizingSettings();    

        Parser.Default.ParseArguments<Options>(args)
                 .WithParsed<Options>(o =>
                 {
                     AddSettings(o, visualizingSettings);
                     var image = tagCloudDrawer.DrawWordsCloud(o.Path, visualizingSettings);
                     tagCloudDrawer.SaveImage(image);
                 });
    }

    private static void AddSettings(Options options, VisualizingSettings visualizingSettings)
    {      
        if (!options.Size.IsEmpty)
            visualizingSettings.ImageSize = options.Size;

        if (!options.ForegroundColor.IsEmpty)
            visualizingSettings.PenColor = options.ForegroundColor;

        if (!options.BackgroundColor.IsEmpty)
            visualizingSettings.BackgroundColor = options.BackgroundColor;

        if (options.Font != null)
            visualizingSettings.Font = options.Font;

        if (!options.Center.IsEmpty && options.Step != 0 && options.DeltaAngle != 0)
            visualizingSettings.PointDistributor = new Spiral(options.Center, options.Step, options.DeltaAngle);

        else
            visualizingSettings.PointDistributor = new Spiral(new Point(visualizingSettings.ImageSize.Width/2, visualizingSettings.ImageSize.Height/2));
    }
}