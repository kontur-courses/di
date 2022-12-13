using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using SimpleInjector;
using TagsCloud.Core;
using TagsCloud.Core.BitmapPainters;
using TagsCloud.Core.Layouters;
using TagsCloud.Core.Settings;
using TagsCloud.Core.TagContainersCreators;
using TagsCloud.Core.TagContainersCreators.TagsPreprocessors;
using TagsCloud.Core.WordFilters;
using TagsCloud.Core.WordReaders;

namespace TagsCloud.CLI;

public class EntryPoint
{
	private static Container container;

	private static void Main(string[] args)
	{
		var imageSettings = ImageSettings.GetDefaultSettings();

		Build();

		var settingsProvider = container.GetInstance<ISettingsSetter<ImageSettings>>();
		settingsProvider.Set(imageSettings);

		var containerCreator = container.GetInstance<ITagContainersProvider>();
		var painter = container.GetInstance<ITagPainter>();
		var saver = container.GetInstance<ImageSaver>();

		var tags = containerCreator.GetContainers();
		var bitmap = painter.Draw(tags);
		saver.Save(bitmap, "Numb");

		Console.WriteLine($"OK> Image save in folder: {saver.Directory}");


		//var words = SplitTextOnWors($"{Environment.CurrentDirectory}\\Numb.txt");
		//Console.WriteLine($"{words}");
	}

	private static string SplitTextOnWors(string path)
	{
		var numb = File.ReadAllLines(path);
		var sb = new StringBuilder();
		foreach (var numbLine in numb)
		{
			var words = numbLine.Split(' ', StringSplitOptions.TrimEntries);
			foreach (var word in words)
			{
				sb.AppendLine(word);
			}
		}

		return sb.ToString();
	}

	private static void Build()
	{
		container = new Container();

		container.Register<ITagPainter, DebugPainter>();
		container.Register<IWordFilter, TempFilter>();
		container.Register<ITagsPreprocessor, TempPreprocessor>();
		container.Register<ITagContainersProvider, TagContainersProvider>();

		var imageSettingsProvider = new SettingsProvider<ImageSettings>();
		container.RegisterInstance(typeof(ISettingsSetter<ImageSettings>), imageSettingsProvider);
		container.RegisterInstance(typeof(ISettingsGetter<ImageSettings>), imageSettingsProvider);
		

		container.RegisterInstance(typeof(IWordReader), new WordReaderFromTxt($"{Environment.CurrentDirectory}\\Numb.txt"));
		container.RegisterInstance(typeof(ICloudLayouter), new CircularCloudLayouter(new Point(0, 0)));
		//container.RegisterInstance(typeof(ICloudLayouter), new SpiralCloudLayouter(new Point(0, 0), 100, 10));
		container.RegisterInstance(typeof(ImageSaver), new ImageSaver(Environment.CurrentDirectory, ImageFormat.Png));

		container.Verify();
	}
}