using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Drawers;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Layouters.CloudLayouters;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Preprocessing;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Text;
using TagsCloudVisualization.VisualizerActions;
using TagsCloudVisualization.VisualizerActions.GuiActions;
using TagsCloudVisualization.WordStatistics;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TagCloudVisualizerTests
    {
        [Test]
        public void TagCloudVisualizer_NotFailsOnLoad() //If preprocessor module file exists (filename located in "RemoveNotNounsPreprocessor")
        {
            Action action = () => TagCloudVisualizerEntryPoint.Main(new string[0]);
            action.Should().NotThrow();
        }

        private class SaveFileStub : ImageSaveAction, IGuiAction
        {
            private readonly string filenameToReturn;

            public SaveFileStub(AppSettings appSettings, string filenameToReturn) : base(appSettings)
            {
                this.filenameToReturn = filenameToReturn;
            }

            public override string GetActionDescription()
            {
                return null;
            }

            public override string GetActionName()
            {
                return null;
            }

            public MenuCategory GetMenuCategory()
            {
                return MenuCategory.File;
            }

            protected override bool TryGetCorrectFileToSave(out string filepath)
            {
                filepath = filenameToReturn;
                return true;
            }
        }

        private static string GetTempFilePathWithExtension(string extension)
        {
            var path = Path.GetTempPath();
            var fileName = Guid.NewGuid() + extension;
            var file = File.Create(Path.Combine(path, fileName), 10);
            file.Close();
            return file.Name;
        }

        [Test]
        public void TagCloudVisualizer_CreatesFileAfterWork()
        {
            var appSettings = new AppSettings(new ImageSettings(), new PictureBoxImageHolder(),
                new Palette(), new Restrictions());

            var textReader = A.Fake<ITextReader>();
            A.CallTo(() => textReader.Formats)
                .Returns(new HashSet<string>() {"txt"});
            A.CallTo(() => textReader.GetAllWords(null))
                .WithAnyArguments()
                .Returns(new[] {"meat", "chicken"});

            var inputPreprocessor = new InputPreprocessor(new[] {new ToLowercasePreprocessor()}, appSettings);

            var statCounter = new StatisticsCounter(new []{new WordCountCollector()});

            ICloudLayouter GetCloudLayouter() => new CircularCloudLayouter(new Point(0, 0));

            var wordSizeChooser = new WordCountSizeChooser();

            var layouter = new WordLayouter(new CloudLayouterConfiguration(GetCloudLayouter), wordSizeChooser);

            var painter = new DefaultWordPainter(appSettings.Palette);

            var drawer = new DefaultWordDrawer(appSettings);

            var tagCloudContainer = new TagCloudContainer(new[] {textReader}, inputPreprocessor,
                statCounter, layouter, painter, drawer);

            var filepath = GetTempFilePathWithExtension(".png");

            var saveFileAction = new SaveFileStub(appSettings, filepath);

            var graphicalVisualizer = new GraphicalVisualizer(new []{saveFileAction}, appSettings, tagCloudContainer);
            appSettings.CurrentInterface = graphicalVisualizer;
            appSettings.CurrentFile = "test.txt";

            saveFileAction.Perform();
            File.Exists(filepath).Should().BeTrue();
            File.Delete(filepath);
        }
    }
}