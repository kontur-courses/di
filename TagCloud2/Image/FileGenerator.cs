using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TagCloud2.Image
{
    public class FileGenerator : IFileGenerator
    {
        public void GenerateFile(string name, IImageFormatter formatter, System.Drawing.Image image)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            image.Save(name, formatter.GetCodec(), formatter.GetParameters());
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}
