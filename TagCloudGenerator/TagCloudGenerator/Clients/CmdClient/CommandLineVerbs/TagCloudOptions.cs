using System;
using System.Collections.Generic;
using System.Drawing;
using CommandLine;
using TagCloudGenerator.GeneratorCore.TagClouds;
using TagCloudGenerator.GeneratorCore.Tags;

// used implicitly by CommandLine lib
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace TagCloudGenerator.Clients.CmdClient.CommandLineVerbs
{
    public abstract class TagCloudOptions<TTagCloud> : ITagCloudOptions<TTagCloud> where TTagCloud : ITagCloud
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

        public TTagCloud ConstructCloud(Color backgroundColor, Dictionary<TagType, TagStyle> tagStyleByTagType) =>
            (TTagCloud)Activator.CreateInstance(typeof(TTagCloud), backgroundColor, tagStyleByTagType);
    }
}