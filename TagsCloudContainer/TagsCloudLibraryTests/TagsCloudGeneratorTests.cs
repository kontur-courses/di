using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudLibrary;
using TagsCloudLibrary.Colorers;
using TagsCloudLibrary.Layouters;
using TagsCloudLibrary.MyStem;
using TagsCloudLibrary.Preprocessors;
using TagsCloudLibrary.Readers;
using TagsCloudLibrary.WordsExtractor;
using TagsCloudLibrary.Writers;

namespace TagsCloudLibraryTests
{
    [TestFixture]
    class TagsCloudGeneratorTests
    {
        private TagsCloudGenerator tagsCloudGenerator;
        private Size imageSize = new Size(10000, 10000);

        [SetUp]
        public void SetUp()
        {
            tagsCloudGenerator = new TagsCloudGenerator(
                new TxtReader(),
                new LiteratureExtractor(),
                new List<IPreprocessor>{
                    new ToLowercase(),
                    new ExcludeBoringWords(
                            new BoringWordsConfig(new List<Word.PartOfSpeech>{Word.PartOfSpeech.Noun})
                        )
                },
                new CircularCloudLayouter(),
                new BlackColorer(),
                new FontFamily("Arial"), 
                new PngWriter()
            );
        }

        [TestCase("test-texts/test1.txt", "test-imgs/test1.png")]
        [TestCase("test-texts/test2.txt", "test-imgs/test2.png")]
        public void Test(string textFile, string expectedImageFile)
        {
            var imageName = Guid.NewGuid().ToString() + ".png";
            tagsCloudGenerator.GenerateFromFile(textFile, imageName,
                imageSize.Width, imageSize.Height);

            File.ReadAllBytes(imageName).Should()
                .BeEquivalentTo(
                    File.ReadAllBytes(expectedImageFile),
                    config => config.WithStrictOrdering(),
                $"Generated image should be equal to expected. Image is saved as {Path.GetFullPath(imageName)}");

            File.Delete(imageName);
        }
    }
}
