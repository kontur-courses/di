using System;
using System.Drawing;
using System.IO;
using System.Text;
using TagCloud.Interfaces.GUI.UIActions;

namespace TagCloud.Interfaces
{
    public class ApplicationSettings
    {
        public string FilePath { get; set; }
        public Encoding TextEncoding { get; set; } = Encoding.Default;
        public Size WindowSize { get; set; }
        public StreamReader GetDocumentStream()
        {
            if (FilePath is null)
                throw new ArgumentNullException("Не задан файл");
            return new StreamReader(File.OpenRead(FilePath), TextEncoding);
        }

    }
}
