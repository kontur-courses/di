using SixLabors.ImageSharp;
using TagsCloudVisualization;

// Layout work demonstration.

var random = new Random();

// Determine screen size and center:
var screenSize = new Size(1920, 1080);
var center = new PointF((float)screenSize.Width / 2, (float)screenSize.Height / 2);

// Create random set of sizes:
var sizes = Enumerable
    .Range(0, 100)
    .Select(rect => new SizeF(random.Next(25, 100), random.Next(25, 100)))
    .ToArray();

// Try sorting for better distribution
Array.Sort(sizes, new SizeFComparer(false));

// Create Archimedes spiral as layoutFunction:
var layoutFunction = new Spiral(0.1f, (float)Math.PI / 180);

// Create layout using layout function and screen center:
var layout = new Layout(layoutFunction, center);

// Put rectangles in layout:
var rects = sizes
    .Select(size => layout.PutNextRectangle(size))
    .ToList();

// Save image of created layout:
LayoutVisualizer.CreateVisualization(
    rects,
    screenSize,
    Color.White,
    1.2f,
    Color.Blue,
    "best_layout.png");