using System.IO;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.UI
{
    public class SetPathToImageAction : ConsoleUiAction
    {
        public override string Category => "AppSettings";
        public override string Name => "SetPathToImage";
        public override string Description { get; }

        public SetPathToImageAction(TextReader reader, TextWriter writer)
            :base(reader, writer)
        {
        }

        public override void Perform()
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