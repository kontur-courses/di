using CommandLine;

namespace TagCloudGenerator.UserInterfaces
{
    public class Options
    {
        [Value(0,
               MetaName = "cloud_vocabulary_filename",
               Required = true,
               HelpText = "Path to file with all cloud words. Each word should be in separate line.")]
        public string CloudVocabularyFilename { get; set; }

        [Option('s', "image_size",
                Default = "800x600",
                HelpText = "Cloud image size in format: [WIDTH]x[HEIGHT]")]
        public string ImageSize { get; set; }
    }
}