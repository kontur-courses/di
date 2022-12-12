using System.Drawing;
using TagsCloud2.TagsCloudMaker.Layouter;
using TagsCloudVisualization;

namespace TagsCloudTests;

public class TestsCreatePicture
{
    public string dirOnThisComputer = Directory.GetCurrentDirectory();
    [Test]
    public void CreatePNG_IdenticalSquares_500Pieces()
    {
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var size = new Size(25, 25);
        var rectangles = new List<Rectangle>();
        var pieces = 500;
        for (var i = 0; i < pieces; i++)
        {
            rectangles.Add(layouter.PutNextRectangle(size));
        }

        var fileName = dirOnThisComputer + "\\500Squares.png";
        var tcv = new TagCloudVisualizer();
        tcv.MakePicture(rectangles, fileName);
    }
    
    [Test]
    public void CreatePNG_IdenticalRectangles_500Pieces()
    {
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var size = new Size(25, 10);
        var rectangles = new List<Rectangle>();
        var pieces = 500;
        for (var i = 0; i < pieces; i++)
        {
            rectangles.Add(layouter.PutNextRectangle(size));
        }

        var fileName = dirOnThisComputer + "\\500Rectangles.png";
        var tcv = new TagCloudVisualizer();
        tcv.MakePicture(rectangles, fileName);
    }

    [Test]
    public void CreatePNG_RandomRectangles_50Big_200Little()
    {
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var rectangles = new List<Rectangle>();
        var littleRectanglePieces = 200;
        var bigRectanglePieces = 50;
        
        var rnd = new Random();
        
        for (var i = 0; i < bigRectanglePieces; i++)
        {
            var size = new Size(rnd.Next(100, 500), rnd.Next(100, 200));
            rectangles.Add(layouter.PutNextRectangle(size));
        }
        
        for (var i = 0; i < littleRectanglePieces; i++)
        {
            var size = new Size(rnd.Next(25, 100), rnd.Next(25, 50));
            rectangles.Add(layouter.PutNextRectangle(size));
        }
        
        var fileName = dirOnThisComputer + "\\250RandomRectangles.png";
        PaintRectangleInFile(rectangles, fileName);
    }

    private static void PaintRectangleInFile(List<Rectangle> rectangles, string fileName)
    {
        var tcv = new TagCloudVisualizer();
        tcv.MakePicture(rectangles, fileName);
    }

    [Test]
    public void CreatePNG_500RandomRectangles()
    {
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var rectangles = new List<Rectangle>();
        var pieces = 500;
        
        var rnd = new Random();
        
        for (var i = 0; i < pieces; i++)
        {
            var size = new Size(rnd.Next(10, 100), rnd.Next(10, 25));
            rectangles.Add(layouter.PutNextRectangle(size));
        }
        var fileName = dirOnThisComputer + "\\500RandomRectangles.png";
        var tcv = new TagCloudVisualizer();
        tcv.MakePicture(rectangles, fileName);
    }
}