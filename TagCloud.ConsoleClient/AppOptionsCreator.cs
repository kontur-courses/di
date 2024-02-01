using System.Drawing;

public static class AppOptionsCreator
{
    public static AppOptions CreateOptions(Options clOptions)
    {
        var tagCloudOptions = CreateTagCloudOptions(clOptions);
        var renderOptions = CreateRenderOptions(clOptions);
        var wordExtractorOptions = CreateWordExtractionOptions(clOptions);
        var serviceOptions = CreateServiceOptions(clOptions);

        return new AppOptions()
        {
            DomainOptions = new DomainOptions()
            {
                WordExtractionOptions = wordExtractorOptions,
                RenderOptions = renderOptions,
                TagCloudOptions = tagCloudOptions,
                ServiceOptions = serviceOptions
            }
        };
    }

    private static TagCloudOptions CreateTagCloudOptions(Options options)
    {
        return new TagCloudOptions 
        { 
            Center = ParsePoint(options.TagCloudCenter), 
            MaxTagsCount = -1 
        };
    }

    private static RenderOptions CreateRenderOptions(Options options)
    {
        var fontSizeSpan = ParseFontSize(options.FontSize);

        return new RenderOptions()
        {
            ColorScheme = ColorScheme.CalmScheme,
            FontBase = CreateFontBase(options.FontFamily ?? "Arial"),
            ImageSize = ParseSize(options.ImageSize),
            MinFontSize = fontSizeSpan.min,
            MaxFontSize = fontSizeSpan.max,
        };
    }
    
    private static Font CreateFontBase(string fontFamily)
    {
        return new Font(fontFamily, 32, FontStyle.Regular, GraphicsUnit.Pixel);
    }

    private static WordExtractionOptions CreateWordExtractionOptions(Options clOptions)
    {
        return new WordExtractionOptions() { MinWordLength = 4, PartsSpeech = PartSpeech.Noun | PartSpeech.Verb };
    }

    private static Size ParseSize(string str)
    {
        if (str is null)
            return new Size(800, 800);

        var coords = str.Split("x").Select(int.Parse).ToArray();
        return new Size(coords[0], coords[1]);
    }

    private static Point ParsePoint(string str)
    {
        if (str is null)
            return new Point(400, 400);

        var coords = str.Split("x").Select(int.Parse).ToArray();
        return new Point(coords[0], coords[1]);
    }

    private static (int min, int max) ParseFontSize(string str)
    {
        if (str is null)
            return (24, 64);

        var sizes = str.Split(":").Select(int.Parse).ToArray();
        return (sizes[0], sizes[1]);
    }

    public static ServiceOptions CreateServiceOptions(Options clOptions)
    {
        return new ServiceOptions()
        {
            FilterType = FilterType.MorphologicalFilter
        };
    }
} 