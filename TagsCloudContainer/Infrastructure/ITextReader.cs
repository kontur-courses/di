using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TagsCloudContainer.Infrastructure
{
    public interface ITextReader
    {
        public string Filter { get; }

        public string ReadText(string path);
    }
}
