using System.IO;

namespace TagsCloudContainer.UI
{
    public class SetPathToTagsTextAction : IUiAction
    {
        private readonly TextWriter writer;
        private readonly TextReader reader;
        public string Category => "AppSettings";
        public string Name => "SetPathToTagsText";
        public string Description { get; }

        public SetPathToTagsTextAction(TextWriter writer, TextReader reader)
        {
            this.writer = writer;
            this.reader = reader;
        }

        public void Perform()
        {
            writer.WriteLine("Set Path To Tags");
            while (true)
            {
                var path = reader.ReadLine();
                if (File.Exists(path))
                {
                    AppSettings.TextFilename = path;
                    return;
                }
                writer.WriteLine("There`re no file by this path, Check it for mistakes");
            }
        }
    }
}