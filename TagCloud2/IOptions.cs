using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public interface IOptions
    {
        string Path { get; set; }

        string Format { get; set; }

        string OutputFormat { get; set; }

        string OutputName { get; set; }

        string FontName { get; set; }

        int FontSize { get; set; }
    }
}
