using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization;

var center = Point.Empty;
var circleLayouter = new CircularCloudLayouter(center);
var rectangles = new List<Rectangle>();
var random = new Random();
for (var i = 0; i < 100; i++)
{
    var rectangle = circleLayouter.PutNextRectangle(new Size(random.Next(10, 100), random.Next(10, 100)));
    rectangles.Add(rectangle);
}

var directoryPath = Path.Join(Environment.CurrentDirectory, "Images");
if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

var filename = "RandomCloud.png";
var fullpath = Path.Combine(directoryPath, filename);
using var image = new ImageGenerator().Generate(rectangles);
image.Save(fullpath, ImageFormat.Png);
Console.WriteLine($"Tag cloud visualization saved to file {fullpath}");