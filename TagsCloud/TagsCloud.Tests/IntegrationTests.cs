using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.GUI;
using TagsCloud.Infrastructure;
using TagsCloud.Layouters;
using TagsCloud.UiActions;
using TagsCloud.WordsProcessing;

namespace TagsCloud.Tests
{
    class IntegrationTests
    {
        [Test]
        public void ImageHolderShould_ChangeLayouter_OnChangeActionCall()
        {
            var holder = A.Fake<IImageHolder>();
            var fakeLayouter = A.Fake<SpiralCloudLayouter>();
            var changeAlgorithmAction = new SelectSpiralLayouterAction(holder, fakeLayouter);
            changeAlgorithmAction.Perform();
            A.CallTo(() => holder.ChangeLayouter(fakeLayouter)).MustHaveHappenedOnceExactly();

        }

        [Test]
        public void LayouterShould_PlaceWords_WhenCalled_RenderWords()
        {
            var layouter = A.Fake<ICloudLayouter>();
            var settings = A.Fake<ImageSettings>(options =>
                options.WithArgumentsForConstructor(new List<object?> {1920, 1080}));

            var holder = A.Fake<IImageHolder>(o =>
                o.Wrapping(A.Fake<PictureBoxImageHolder>(opt =>
                    opt.WithArgumentsForConstructor(new List<object?> {settings, layouter}))));

            var frequencies = new Dictionary<string, int> {{"word", 145}, {"second", 267}, {"third", 78}};
            holder.RenderWords(frequencies);
            A.CallTo(() => layouter.PutNextRectangle(Size.Empty)).WithAnyArguments().MustHaveHappened(3, Times.Exactly);
        }

        [Test]
        public void LayouterShould_UpdateCenterPoint_WhenChangedResolution()
        {
            var layouter = A.Fake<ICloudLayouter>();
            var settings = A.Fake<ImageSettings>(options =>
                options.WithArgumentsForConstructor(new List<object?> { 1920, 1080 }));

            var holder = A.Fake<IImageHolder>(o =>
                o.Wrapping(A.Fake<PictureBoxImageHolder>(opt =>
                    opt.WithArgumentsForConstructor(new List<object?> { settings, layouter }))));

            settings.Width = 1720;
            holder.RecreateCanvas(settings);
            A.CallTo(() => layouter.UpdateCenterPoint(settings)).MustHaveHappenedTwiceExactly();
        }

        [Test]
        public void MainFormShould_AdjustResolution_ToStartSettings()
        {
            var layouter = A.Fake<ICloudLayouter>();
            var settings = A.Fake<ImageSettings>(options =>
                options.WithArgumentsForConstructor(new List<object?> {1920, 1080}));
            var holder = new PictureBoxImageHolder(settings, layouter);
            var mainForm = new MainForm(new IUiAction[0], holder, settings);
            mainForm.ClientSize.Should().Be(new Size(settings.Width, settings.Height));
        }

        [Test]
        public void WordsFrequencyParserShould_CallToWordsFilter_WhenParsingWords()
        {
            var path = Assembly.GetExecutingAssembly().Location + "testText.txt";
            var words = new List<string> { "one", "two", "three" };

            var filter = A.Fake<IWordsFilter>();
            var parser = A.Fake<IWordsFrequencyParser>(opts => 
                opts.Wrapping(new WordsFrequencyParser(filter)));

            File.WriteAllText(path, string.Join(Environment.NewLine, words));
            parser.ParseWordsFrequencyFromFile(path);
            A.CallTo(() => filter.GetCorrectWords(words)).WithAnyArguments().MustHaveHappenedOnceExactly();
        }
    }
}
