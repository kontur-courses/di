using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.IServices;

namespace TagCloud
{
    public class Settings
    {
        public int Width { get; private set; } = 1000;
        public int Height { get; private set; } = 1000;
        public string PathToFileWithWords = "somepath.txt";
        public Settings(int width,int height)
        {
            Width = width;
            Height = height;
        }
        public void Load(IClientDataFactory clientDataFactory)
        {
            clientDataFactory.settings = this;
        }
    }
}
