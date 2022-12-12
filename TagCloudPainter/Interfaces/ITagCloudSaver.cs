using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudPainter.Interfaces
{
    public interface ITagCloudSaver
    {
        void SaveTagCloud(string outputPath, string inputPath);
    }
}
