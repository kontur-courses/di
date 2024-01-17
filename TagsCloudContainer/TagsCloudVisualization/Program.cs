using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.PointsProviders;

var center = new Point(-1000, 1000);
var layouter = new CircularCloudLayouter(new ArchimedeanSpiralPointsProvider(center));
var rand = new Random();
for (var i = 0; i < 100; i++)
    layouter.PutNextRectangle(new Size(rand.Next(60, 140), rand.Next(20, 80)));

var visualizator = new TagsCloudVisualizator(layouter);
var image = visualizator.Draw();
image.SaveAs(@"../", "layout", ImageFormat.Png);
