using System.IO;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.UI
{
    public class SetPathToImageAction : IUiAction
    {
        private readonly TextWriter writer;
        private readonly TextReader reader;
        public string Category => "AppSettings";
        public string Name => "SetPathToImage";
        public string Description { get; }

        public SetPathToImageAction(TextWriter writer, TextReader reader)
        {
            this.writer = writer;
            this.reader = reader;
        }

        public void Perform()
        {
            writer.WriteLine("Set Path To Image");
            while (true)
            {
                var path = reader.ReadLine();
                var r = new Regex(@"^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([^/:*?<>""|]*))+)$");
                if (r.IsMatch(path))
                {
                    AppSettings.ImageFilename = path;
                    return;
                }
                writer.WriteLine("It`s not good path to file, Check it for mistakes");
            }
        }
    }
}