using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.UI
{
    public interface IInitialSettings : ICloneable
    {
        string InputFilePath { get; set; }
        string OutputFilePath { get; set; }
        Size ImageSize { get; set; }
    }
}
