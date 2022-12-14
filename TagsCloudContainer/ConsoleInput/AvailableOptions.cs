using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization;

namespace TagsCloudContainer;
#pragma warning disable CA1416
public static class AvailableOptions
{
    public static readonly Dictionary<string, Brush> AvailableBrushes = new();
    public static readonly Dictionary<string?, ISizeManager> AvailableLayouters = new();
    public static readonly HashSet<string?> AvailableFonts = new();
    public static readonly HashSet<string> AvailableSpPartsToIgnore = new();
    public static readonly HashSet<string> AvailableSaveFormats = new();


    static AvailableOptions()
    {
        AvailableBrushes.Add("красный", Brushes.Red);
        AvailableBrushes.Add("синий", Brushes.Blue);
        AvailableBrushes.Add("черный", Brushes.Black);
        AvailableBrushes.Add("желтый", Brushes.Yellow);
        AvailableBrushes.Add("зеленый", Brushes.Green);

        AvailableLayouters.Add("Circle", AppDIInitializer.Container.GetService<ISizeManager>());

        AvailableFonts.Add("Times New Roman");
        AvailableFonts.Add("Arial");
        AvailableFonts.Add("Georgia");

        AvailableSpPartsToIgnore.Add("предл");
        AvailableSpPartsToIgnore.Add("мест");
        AvailableSpPartsToIgnore.Add("сущ");
        AvailableSpPartsToIgnore.Add("гл");
        AvailableSpPartsToIgnore.Add("прил");
        AvailableSpPartsToIgnore.Add("нар");

        AvailableSaveFormats.Add("png");
        AvailableSaveFormats.Add("jpg");
        AvailableSaveFormats.Add("bmp");
        AvailableSaveFormats.Add("tiff");
        AvailableSaveFormats.Add("raw");
    }
}
#pragma warning restore CA1416