using CommandLine;

namespace TagsCloudContainer
{
    public class Options
    {
        [Option('p', "path", Required = true, HelpText = "Путь до файла с текстом")]
        public string PathToFile { get; set; }
        [Option('n', "name", Required = true, HelpText = "Имя сохраняемого файла(расширение указывать не надо)")]
        public string PathSaveFile { get; set; }
    }
}