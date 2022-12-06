using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudGraphicalUserInterface
{
    public class TagCloudSettings : IDirectoryProvider
    {
        public Point StartPoint { get; set; } = new Point(0, 0);
        public int ellipsoide { get; set; } =1;
        public int desteny { get; set; } = 1;
        public string ImagesDirectory { get; set; }
        public int maxFont { get; set; } = 30;
        public int minFont { get; set; } = 20;
        public FontFamily Font { get; set; }= FontFamily.GenericSansSerif;

    }
}
