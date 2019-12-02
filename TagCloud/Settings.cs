using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    public static class Settings
    {
        public static int Count { get; private set; } = 50;
        public static Point Center { get; private set; } = new Point(50, 50);
        public static int Width { get; private set; } = 1000;
        public static int Height { get; private set; } = 1000;
        public static string PathToFileWithWords = "somepath.txt";
        public static class SaveImageSettings
        {
            public static ImageFormat SaveFormat { get; private set; } = ImageFormat.Png;
            public static string SavePath { get; private set; } = Path.GetTempPath();
        }
        //public Settings (int count, Point center)
        //{
        //    this.Count = count;
        //    this.Center = center;
        //}
    }
}
