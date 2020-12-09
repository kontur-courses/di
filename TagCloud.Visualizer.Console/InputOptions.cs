using CommandLine;

namespace TagCloud.Visualizer.Console
{
    public class InputOptions
    {
        [Option('f', "fileExtension")] public string FileExtension { get; private set; }
        [Option("fileName")] public string FileName { get; private set; }

        public bool IsDocOrDocx => FileExtension == "doc" || FileExtension == "docx";

        public InputOptions()
        {
            FileExtension = "txt";
            FileName = "input";
        }

        public InputOptions(string fileName, string fileExtension)
        {
            FileName = fileName;
            FileExtension = fileExtension;
        }

        public int ChangeInputOptionsAndReturnExitCode(InputOptions opts)
        {
            FileName = opts.FileName;
            FileExtension = opts.FileExtension;
            return 0;
        }
    }
}