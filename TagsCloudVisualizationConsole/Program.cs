using System.Drawing;
using TagsCloudVisualization;
using TagsCloudVisualization.Interfaces;

if (!Directory.Exists("Images"))
    Directory.CreateDirectory("Images");

ITagsCloudVisualization tagsCloudVisualization = new BitmapTagsCloudVisualization();

var random = new Random();

#region SquareRectLayout

var squareRectLayout = new CircularCloudLayouter(new Point(0, 0), 0.01);

for (int i = 0; i < 100; i++)
{
    var squareWidth = random.Next(5, 100);
    squareRectLayout.PutNextRectangle(new Size(squareWidth, squareWidth));
}

tagsCloudVisualization.SaveTagsCloud(squareRectLayout, "Images\\SquareRectLayout.bmp");

#endregion


#region HorizontalRectLayout

var horizontalRectLayout = new CircularCloudLayouter(new Point(0, 0), 0.01);

for (int i = 0; i < 100; i++)
{
    var height = random.Next(5, 25);
    var width = random.Next(50, 100);
    horizontalRectLayout.PutNextRectangle(new Size(width, height));
}

tagsCloudVisualization.SaveTagsCloud(horizontalRectLayout, "Images\\HorizontalRectLayout.bmp");

#endregion


#region VerticalRectLayout

var verticalRectLayout = new CircularCloudLayouter(new Point(0, 0), 0.01);

for (int i = 0; i < 100; i++)
{
    var width = random.Next(5, 25);
    var height = random.Next(50, 100);
    verticalRectLayout.PutNextRectangle(new Size(width, height));
}

tagsCloudVisualization.SaveTagsCloud(verticalRectLayout, "Images\\VerticalRectLayout.bmp");

#endregion


#region SingleSizeSquareRectLayout

var singleSizeSquareRectLayout = new CircularCloudLayouter(new Point(0, 0), 0.01);
var squareSingleWidth = random.Next(5, 100);

for (int i = 0; i < 100; i++)
{
    singleSizeSquareRectLayout.PutNextRectangle(new Size(squareSingleWidth, squareSingleWidth));
}

tagsCloudVisualization.SaveTagsCloud(singleSizeSquareRectLayout, "Images\\SingleSizeSquareRectLayout.bmp");

#endregion