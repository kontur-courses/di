using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class FileStreamReader : IStreamReader
    {
        private readonly IFileProvider fileProvider;
        private readonly IWordProvider wordProvider;

        public FileStreamReader(IFileProvider fileProvider, IWordProvider wordProvider)
        {
            this.fileProvider = fileProvider;
            this.wordProvider = wordProvider;
        }

        public void Read()
        {
            var line = fileProvider.File.ReadLine();
            while (line != null)
            {
                wordProvider.Words.Add(line);
                line = fileProvider.File.ReadLine();
            }
        }
    }
}