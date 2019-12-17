using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using Autofac;
using System.IO;
using System;

namespace TagsCloudContainer
{
    public class TagCloudBuilder : ITagCloudBuilder
    {
        private readonly TextHandler fileHandler;
        private readonly ITagCloudBuildingAlgorithm algorithmToBuild;

        public TagCloudBuilder(TextHandler fileHandler,
            ITagCloudBuildingAlgorithm algorithmToBuild)
        {
            this.fileHandler = fileHandler;
            this.algorithmToBuild = algorithmToBuild;
        }

        public IEnumerable<Tag> GetTagsCloud()
        {
            var frequencyDict = fileHandler.GetWordsFrequencyDict();
            return algorithmToBuild.GetTags(frequencyDict);
        }
    }
}