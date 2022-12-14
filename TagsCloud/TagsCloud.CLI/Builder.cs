using System.Drawing;
using System.Drawing.Imaging;
using SimpleInjector;
using TagsCloud.Core;
using TagsCloud.Core.Layouters;
using TagsCloud.Core.Painters;
using TagsCloud.Core.Settings;
using TagsCloud.Core.TagContainersProviders;
using TagsCloud.Core.TagContainersProviders.TagsPreprocessors;
using TagsCloud.Core.WordFilters;
using TagsCloud.Core.WordReaders;
using TagsCloud.Core.WordTransformers;

namespace TagsCloud.CLI;

public static class Builder
{
	public static Container Build(Options options)
	{
		var container = new Container();

		RegisterWordsFilters(container, options);
		RegisterTransformers(container);
		RegisterPreprocessor(container, options.PathToWordsFile);
		RegisterImageSettingsProvider(container);
		RegisterLayouter(container, options);


		container.Register<ITagsCloudPainter, TagsCloudPainter>();
		container.Register<ITagContainersProvider, TagContainersProvider>();

		container.RegisterInstance(typeof(ImageSaver), new ImageSaver(options.PathToImage, ImageFormat.Png));

		container.Verify();

		return container;
	}

	private static void RegisterPreprocessor(Container container, string pathToWords)
	{
		container.Register<IWordReader>(() => new WordReaderFromTxt(pathToWords));
		container.Register<ITagsPreprocessor, TagPreprocessor>();
	}

	private static void RegisterWordsFilters(Container container, Options options)
	{
		var filters = new List<IWordFilter> { new MinLengthFilter(3) };

		if (options.PathToExcludedWords is not null)
		{
			var boringWordsReader = new WordReaderFromTxt(options.PathToExcludedWords);
			filters.Add(new BoringWordsFilter(boringWordsReader));
		}

		container.Collection.Register<IWordFilter>(filters);

		container.Register<IWordFiltersComposer, WordFilterComposer>();
	}

	private static void RegisterImageSettingsProvider(Container container)
	{
		var imageSettingsProvider = new SettingsProvider<ImageSettings>();
		container.RegisterInstance(typeof(ISettingsSetter<ImageSettings>), imageSettingsProvider);
		container.RegisterInstance(typeof(ISettingsGetter<ImageSettings>), imageSettingsProvider);
	}

	private static void RegisterTransformers(Container container)
	{
		container.Collection.Register<IWordTransformer>(new ToLowerTransformer(0));
		container.Register<IWordTransformersComposer, WordTransformersComposer>();
	}

	private static void RegisterLayouter(Container container, Options options)
	{
		ICloudLayouter layouter = options.Layouter switch
		{
			"spiral" => new SpiralCloudLayouter(new Point(0, 0), 100, 10),
			_ => new CircularCloudLayouter(new Point(0, 0))
		};

		container.RegisterInstance(typeof(ICloudLayouter), layouter);
	}
}