using System.Drawing;
using TagsCloudVisualization;

const int
    BITMAP_WIDTH = 300,
    BITMAP_HEIGHT = 300,
    CENTER_X = 150,
    CENTER_Y = 150;

var layouter = new CircularCloudLayouterSpiral(new Point(CENTER_X, CENTER_Y));
var drawer = new TagCloudDrawer(
    BITMAP_WIDTH, BITMAP_HEIGHT,
    1, Color.Black,
    layouter);
drawer.DrawRandomRectangles(200,
    new Size(1, 1),
    new Size(10, 10));
drawer.SaveImage();