using CommandLine;

// used implicitly by CommandLine lib
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace TagCloudGenerator.UserInterfaces.CmdClient.CommandLineVerbs
{
    public abstract class TagCloudOptions : ITagCloudOptions
    {
        [Value(0,
               MetaName = "cloud_vocabulary_filename",
               Required = true,
               HelpText = "Path to file with all cloud words. Each word should be in separate line.")]
        public string CloudVocabularyFilename { get; set; }

        [Option('s', "image_size",
                Default = "800x600",
                HelpText = "Cloud image size in format: [width]x[height]")]
        public string ImageSize { get; set; }

        [Option('e', "excluded_vocabulary_filename",
                HelpText = "Words from this file will be excluded from cloud.")]
        public string ExcludedWordsVocabularyFilename { get; set; }

        public abstract string ImageFilename { get; }
        public abstract int GroupsCount { get; }
        public abstract string MutualFont { get; set; }
        public abstract string BackgroundColor { get; set; }
        public abstract string FontSizes { get; set; }
        public abstract string TagColors { get; set; }
    }
}