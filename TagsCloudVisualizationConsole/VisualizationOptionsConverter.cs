using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudVisualizationConsole;

public static class VisualizationOptionsConverter
{
    public static VisualizationOptions ConvertOptions(ArgsOptions argsOptions)
    {
        var visualizationsOptions = new VisualizationOptions();

        if (!Directory.Exists(argsOptions.DirectoryToMyStemProgram))
            throw new ArgumentException($"Directory for mystem not exist {argsOptions.DirectoryToMyStemProgram}");

        if (argsOptions.CanvasWidth < 1 || argsOptions.CanvasHeight < 1)
            throw new ArgumentException("Canvas width and height must be greater than 1");

        var backgroundColor = Color.FromName(argsOptions.BackgroundColor);
        if (!backgroundColor.IsKnownColor)
            throw new ArgumentException("Invalid background color");

        if (string.IsNullOrEmpty(argsOptions.FontFamily))
            throw new ArgumentException($"FontFamily can't be null");

        var fontFamily = new FontFamily(argsOptions.FontFamily);
        if (fontFamily.Name != argsOptions.FontFamily)
            throw new ArgumentException($"Font \"{argsOptions.FontFamily}\" can't be found");

        if (argsOptions.MinFontSize < 1)
            throw new ArgumentException("MinFontSize must be greater than 1");

        if (argsOptions.MaxFontSize <= argsOptions.MinFontSize)
            throw new ArgumentException("MaxFontSize  must be greater than MinFontSize");

        var defaultPaletteBrush = Color.FromName(argsOptions.PaletteDefaultBrush);
        if (!defaultPaletteBrush.IsKnownColor)
            throw new ArgumentException("Invalid default palette brush");
        var palette = new Palette(new SolidBrush(defaultPaletteBrush));

        foreach (var colorName in argsOptions.PaletteAvailableBrushes)
        {
            var color = Color.FromName(colorName);
            if (!color.IsKnownColor)
                throw new ArgumentException($"Invalid color {color} for palette");
            palette.AvailableBrushes.Add(new SolidBrush(color));
        }

        visualizationsOptions.CanvasSize = new Size(argsOptions.CanvasWidth, argsOptions.CanvasHeight);
        visualizationsOptions.SpiralStep = argsOptions.SpiralStep;
        visualizationsOptions.BackgroundColor = backgroundColor;
        visualizationsOptions.TakeMostPopularWords = argsOptions.TakeMostPopularWords;
        visualizationsOptions.BoringWords = argsOptions.BoringWords;
        visualizationsOptions.ExcludedPartsOfSpeech = argsOptions.ExcludedPartsOfSpeech;
        visualizationsOptions.FontFamily = fontFamily;
        visualizationsOptions.MinFontSize = argsOptions.MinFontSize;
        visualizationsOptions.MaxFontSize = argsOptions.MaxFontSize;
        visualizationsOptions.Palette = palette;
        visualizationsOptions.DirectoryToMyStemProgram = argsOptions.DirectoryToMyStemProgram;

        return visualizationsOptions;
    }
}