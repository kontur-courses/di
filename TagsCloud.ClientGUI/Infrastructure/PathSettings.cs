using System.IO;

namespace TagsCloud.ClientGUI.Infrastructure
{
    public class PathSettings
    {
        private static readonly char Sep = Path.DirectorySeparatorChar;

        public string PathToText { get; set; } =
            Path.Join(Directory.GetCurrentDirectory(), $"..{Sep}..{Sep}..{Sep}", $"Texts{Sep}SourceText2.txt");

        public string PathToBoringWords { get; set; } =
            Path.Join(Directory.GetCurrentDirectory(), $"..{Sep}..{Sep}..{Sep}", $"Texts{Sep}BoringWords.txt");

        public string PathToDictionary { get; set; } =
            Path.Join(Directory.GetCurrentDirectory(), $"..{Sep}..{Sep}..{Sep}", $"Texts{Sep}ru_RU.dic");

        public string PathToAffix { get; set; } =
            Path.Join(Directory.GetCurrentDirectory(), $"..{Sep}..{Sep}..{Sep}", $"Texts{Sep}ru_RU.aff");
    }
}