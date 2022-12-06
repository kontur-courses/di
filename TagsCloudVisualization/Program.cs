using System;
using System.Drawing;
using System.IO;

namespace TagsCloudVisualization
{
    public class Program
    {
        private static readonly string ProjectDirectory 
            = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        
        public static void Main()
        {
            var center = new Point(750, 750);
            var cloudLayouter = new CircularCloudLayouter();
            var listSize = TagCloudHelper.GenerateRandomListSize(150);
            var rectangles = cloudLayouter.GenerateCloud(center, listSize);
            var bitmap = TagCloudHelper.DrawTagCloud(rectangles, 1500, 1500);
            
            bitmap.Save(string.Concat(ProjectDirectory, @"\Images\cloud.png"));
        }
    }
}