using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    public class ClientData
    {
        public int Width { get; set; } = 1000;
        public int Height { get; set; } = 1000;
        public string Path { get; set; } = "defaultPath";
        public ClientData(int width,int height)
        {
            Width = width;
            Height = height;
        }
        public ClientData() { }
    }
}
