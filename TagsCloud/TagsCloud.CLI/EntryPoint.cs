using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;
using TagsCloud.Core;
using TagsCloud.Core.Painters;
using TagsCloud.Core.Painters.Pallets;
using TagsCloud.Core.Settings;
using TagsCloud.Core.TagContainersProviders;

namespace TagsCloud.CLI;

public class EntryPoint
{
	private static void Main(string[] args)
	{
		if (args.Length == 0)
		{
			args = new[] { "-wNumb.txt", "-sExample.png", "--excluding=BoringWords.txt" };
			Console.WriteLine(
				"Example with default options: \"-w Numb.txt -s Example.png --excluding BoringWords.txt\"");
		}

		Parser.Default.ParseArguments<Options>(args)
			.WithParsed(RunOptions);
	}

	private static void RunOptions(Options options)
	{
		var container = Builder.Build(options);

		var imageSettings = GetImageSettings(options);

		var settingsProvider = container.GetInstance<ISettingsSetter<ImageSettings>>();
		settingsProvider.Set(imageSettings);

		var containerCreator = container.GetInstance<ITagContainersProvider>();
		var painter = container.GetInstance<ITagsCloudPainter>();
		var saver = container.GetInstance<ImageSaver>();

		var tags = containerCreator.GetContainers();
		var bitmap = painter.Draw(tags);
		saver.Save(bitmap);

		Console.WriteLine($"OK> Image save as {options.PathToImage}");
	}

	private static ImageSettings GetImageSettings(Options options)
	{
		var fontColor = options.FontColor is null ? Color.BlueViolet : Color.FromName(options.FontColor);
		var backgroundColor =
			options.BackgroundColor is null ? Color.AliceBlue : Color.FromName(options.BackgroundColor);
		ITagCLoudPallet pallet = options.UseRandomColor
			? new RandomPallet(backgroundColor)
			: new MonocolorPallet(fontColor, backgroundColor);

		var fontFamily = options.FontName is null ? FontFamily.GenericMonospace : new FontFamily(options.FontName);
		var fontSize = options.MinFontSize ?? 14;

		var width = options.ImageWidth ?? 1000;
		var height = options.ImageHeight ?? 1000;

		return new ImageSettings
		{
			ImageSize = new Size(width, height),
			FontFamily = fontFamily,
			MinFontSize = fontSize,
			Format = ImageFormat.Png,
			Pallet = pallet
		};
	}
}