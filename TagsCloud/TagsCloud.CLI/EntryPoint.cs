using System.Drawing;
using System.Drawing.Imaging;
using SimpleInjector;
using TagsCloud.Core;
using TagsCloud.Core.BitmapPainters;
using TagsCloud.Core.Layouters;
using TagsCloud.Core.TagContainersCreators;
using TagsCloud.Core.TagContainersCreators.TagsPreproessors;
using TagsCloud.Core.WordFilters;
using TagsCloud.Core.WordReaders;

namespace TagsCloud.CLI;

public class EntryPoint
{
	private static Container container;

	private static void Main(string[] args)
	{
		var width = 1600;
		var height = 1600;

		Build(new Point(width / 2, height / 2));

		var containerCreator = container.GetInstance<ITagContainersCreator>();
		var painter = container.GetInstance<ITagPainter>();
		var saver = container.GetInstance<ImageSaver>();

		var tags = containerCreator.Create();
		var bitmap = painter.Draw(tags, new Size(width, height));
		saver.Save(bitmap, "TEST");

		Console.WriteLine($"Image save in folder: {saver.Directory}");
	}

	private static void Build(Point imageCenter)
	{
		container = new Container();

		container.Register<ITagPainter, TempPainter>();
		container.Register<IWordReader, TempReader>();
		container.Register<IWordFilter, TempFilter>();
		container.Register<ITagsPreprocessor, TempPreprocessor>();
		container.Register<ITagContainersCreator, TempCreator>();
		container.RegisterInstance(typeof(ICloudLayouter), new CircularCloudLayouter(imageCenter));
		container.RegisterInstance(typeof(ImageSaver), new ImageSaver(Environment.CurrentDirectory, ImageFormat.Png));

		container.Verify();
	}
}