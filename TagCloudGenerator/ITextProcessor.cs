using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloudGenerator
{
    public interface ITextProcessor
    {
        string ProcessTheText(string strFilePath);
    }
}
