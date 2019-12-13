using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Reflection;
using TagsCloud;
using TagsCloud.DI;
using TagsCloud.Layouters;
using TagsCloud.Renderers;
using TagsCloud.WordsFiltering;

namespace TagsCloud_Tests
{
    public class TagsCloudTests
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void AllProcessTest()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudModule());
            builder.RegisterType<TestingLayouter>().As<ITagsCloudLayouter>().SingleInstance();
            builder.RegisterType<ColoredRenderer>().As<ITagsCloudRenderer>().SingleInstance();
            var container = builder.Build();

            var inputFileName = Path.ChangeExtension(Path.GetTempFileName(), ".txt");
            File.WriteAllText(inputFileName, "Съешь ещё этих мягких французских булок, да выпей же чаю");

            var wordsLoader = container.Resolve<WordsLoader>();
            var words = wordsLoader.LoadWords(inputFileName);

            var filters = container.Resolve<IFilter[]>()
                .Where(f => f.GetType() == typeof(WasteWordsFilter) || f.GetType() == typeof(UpperCaseFilter))
                .OrderByDescending(f => f.GetType().Name).ToArray();
            var wordsFilterer = container.Resolve<WordsFilterer>(new NamedParameter("filters", filters));
            var filteredWords = wordsFilterer.FilterWords(words);

            var layouter = container.Resolve<ITagsCloudLayouter>();
            var renderer = container.Resolve<ITagsCloudRenderer>();
            var tagCloud = container.Resolve<TagsCloudGenerator>(
                new NamedParameter("layouter", layouter as ITagsCloudLayouter),
                new NamedParameter("renderer", renderer as ITagsCloudRenderer));

            var image = tagCloud.GenerateCloud(filteredWords);

            var filename = Path.ChangeExtension(Path.GetTempFileName(), ".png");

            var imageSaveHelper = container.Resolve<ImageSaveHelper>();
            imageSaveHelper.SaveTo(image, filename);

            Approvals.VerifyFile(filename);
        }
    }
}
