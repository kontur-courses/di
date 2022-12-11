using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Interfaces
{
    public interface ITagCloud
    {
        public void PrintTagCloud(string textFilePath, string exportFilePath, string extension);
    }
}
