using System.IO;

namespace TagsCloudContainer.UI
{
    public class SetPathToTagsTextAction : ConsoleUiAction
    {
        public override string Category => "AppSettings";
        public override string Name => "SetPathToTagsText";
        public override string Description { get; }

        public SetPathToTagsTextAction(TextReader reader, TextWriter writer)
            :base(reader, writer)
        {
        }

        public override void Perform()
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