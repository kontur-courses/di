using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Tests
{
    public class TagsCloudTest
    {
        private TagsCloud tagsCloud;
        private const string notDefaultFilename = "test_output.png";

        [SetUp]
        public void SetUp()
        {
            tagsCloud = new TagsCloud();
            DeleteFiles(tagsCloud.Settings.Saving.OutputFile, notDefaultFilename);
            File.WriteAllLines(tagsCloud.Settings.FileLoading.FileName, TestWords.AboutSpace);
        }

        [Test]
        public void Render_WithDefaultSettings_RenderImage()
        {
            var outputPath = tagsCloud.Settings.Saving.OutputFile;
            tagsCloud.Render();
            File.Exists(outputPath).Should().BeTrue();
        }

        [Test]
        public void Render_WithChangedSettings_RenderImage()
        {
            tagsCloud.Settings.Saving.OutputFile = notDefaultFilename;
            tagsCloud.Render();
            File.Exists(notDefaultFilename).Should().BeTrue();
        }

        [Test]
        public void Render_WithReplacedSettings_RenderImage()
        {
            tagsCloud.Settings.Saving = new DefaultSaveSettings {OutputFile = notDefaultFilename};
            tagsCloud.Render();
            File.Exists(notDefaultFilename).Should().BeTrue();
        }

        [Test]
        public void SettingSettings_WithWrongValues_ThrowsException()
        {
            Assert.Throws<ApplicationException>(() =>
                tagsCloud.Settings.Rendering.DesiredImageSize = new Size(-1, -1));
        }

        private static void DeleteFiles(params string[] filenames)
        {
            foreach (var filename in filenames)
            {
                if (File.Exists(filename))
                    File.Delete(filename);
            }
        }
    }
}