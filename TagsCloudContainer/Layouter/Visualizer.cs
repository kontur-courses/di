using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Layouter
{
    public class Visualiser
    {
        public static Bitmap DrawRectangles(CircularCloudLayouter ccl, int bitmapWidth = 4000, int bitmapHeight = 4000)
        {
            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            var graphics = Graphics.FromImage(bitmap);
            var brush = new SolidBrush(Color.Black);
            var pen = new Pen(Color.Red, 10);
            graphics.Clear(Color.White);
            graphics.FillRectangles(brush, ccl.RectanglesList.ToArray());
            graphics.DrawRectangles(pen, ccl.RectanglesList.ToArray());
            return bitmap;
        }

//        public static void Main(string[] args)
//        {
//            if (args.Length == 0 || (args.Length < 4 && args.Length != 0 && args[0] != "-l" && args[0] != "-r"))
//            {
//                Console.WriteLine("Please enter name of file, count of rectangles and size (width, height)");
//                Console.WriteLine("Usage: TagsCloudVisualization <file name> <count> <width> <height>");
//                Console.WriteLine(
//                    "Or use flag -l to enter name of file, and sizes separated by space (width, height)");
//                Console.WriteLine(
//                    "Usage: TagsCloudVisualization -l <file name> <width> <height> <width> <height>...");
//                Console.WriteLine(
//                    "Also you can use flag -r to generate random sizes, enter name of file, and count");
//                Console.WriteLine("Usage: TagsCloudVisualization -r <file name> <count>");
//                return;
//            }
//
//            if (args.Length == 4 && args[0] != "-l")
//            {
//                try
//                {
//                    var fileName = args[0];
//                    var count = int.Parse(args[1]);
//                    var width = int.Parse(args[2]);
//                    var height = int.Parse(args[3]);
//                    var oneSizedCcl = new CircularCloudLayouter(new Point(2000, 2000));
//                    for (var i = 0; i < count; i++)
//                    {
//                        oneSizedCcl.PutNextRectangle(new Size(width, height));
//                    }
//
//                    var bitmap = DrawRectangles(oneSizedCcl);
//                    bitmap.Save(fileName, ImageFormat.Png);
//                    return;
//                }
//                catch (FormatException)
//                {
//                    Console.WriteLine("Please enter a numeric argument.");
//                    Console.WriteLine("Usage: TagsCloudVisualization <file name> <count> <width> <height>");
//                    return;
//                }
//            }
//
//            if (args[0] == "-l")
//            {
//                try
//                {
//                    var fileName = args[1];
//                    var oneSizedCcl = new CircularCloudLayouter(new Point(2000, 2000));
//                    for (var i = 2; i < args.Length - 1; i += 2)
//                    {
//                        var width = int.Parse(args[i]);
//                        var height = int.Parse(args[i + 1]);
//                        oneSizedCcl.PutNextRectangle(new Size(width, height));
//                    }
//
//                    var bitmap = DrawRectangles(oneSizedCcl);
//                    bitmap.Save(fileName, ImageFormat.Png);
//                    return;
//                }
//                catch (Exception)
//                {
//                    Console.WriteLine(
//                        "Usage: TagsCloudVisualization -l <file name> <width> <height> <width> <height>...");
//                    return;
//                }
//            }
//
//            if (args[0] == "-r")
//            {
//                try
//                {
//                    var randomCcl = new CircularCloudLayouter(new Point(2000, 2000));
//                    var rnd = new Random();
//                    var fileName = args[1];
//                    var count = int.Parse(args[2]);
//                    for (var i = 0; i < count; i++)
//                    {
//                        var height = rnd.Next(50, 200);
//                        var width = rnd.Next(50, 200);
//                        var rectSize = new Size(height, width);
//                        randomCcl.PutNextRectangle(rectSize);
//                    }
//
//                    var bitmap = DrawRectangles(randomCcl);
//                    bitmap.Save(fileName, ImageFormat.Png);
//                    return;
//                }
//                catch (Exception)
//                {
//                    Console.WriteLine("Usage: TagsCloudVisualization -r <file name> <count>");
//                    return;
//                }
//            }
//        }
    }
}