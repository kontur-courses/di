using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.TextTools
{
    public interface IFileReader
    {
        public abstract string ReadTextFromFile(string filePath);
    }
}
