using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Enter folder path");
            var folderPath = Console.ReadLine();
            Console.WriteLine(@"Enter min and max edge length of a rectangle");
            var inputStr = Console.ReadLine().Split();
            var minEdgeRectangle = int.Parse(inputStr[0]);
            var maxEdgeRectangle = int.Parse(inputStr[1]);
            Console.WriteLine(@"Enter center coordinates");
            inputStr = Console.ReadLine().Split();
            var x = int.Parse(inputStr[0]);
            var y = int.Parse(inputStr[1]);
            var center = new Point(x, y);
            Console.WriteLine(@"Enter the number of rectangles");
            var numberRectangles = int.Parse(Console.ReadLine());
            var bmp = new CircularCloudVisualizer(new Pen(Brushes.DarkOrchid, 5))
                .DrawRandomRectanglesInBitmap(minEdgeRectangle, maxEdgeRectangle, center, numberRectangles);
            bmp.Save(folderPath ?? throw new InvalidOperationException("Folder path is null."));
        }
    }
}
