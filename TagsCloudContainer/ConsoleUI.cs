using System.Drawing.Imaging;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class ConsoleUI : IUI
{
    private Settings settings;
    private IParser parser;
    private IPreprocessorsApplier applier;
    private ITagCreator creator;
    private ITagPainter painter;
    private ISpiral spiral;
    
    public ConsoleUI(Settings settings, IParser parser,
        IPreprocessorsApplier applier, ITagCreator creator, 
        ITagPainter painter, ISpiral spiral)
    {
        this.settings = settings;
        this.parser = parser;
        this.applier = applier;
        this.creator = creator;
        this.painter = painter;
        this.spiral = spiral;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1 - Image settings");
            Console.WriteLine("2 - Cloud settings");
            Console.WriteLine("3 - Generate image");
            Console.WriteLine("4 - Exit");
            var answer = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
            switch (answer.KeyChar)
            {
                case '1':
                    ImageSettingsKey();
                    break;
                case '2':
                    CloudSettingsKey();
                    break;
                case '3':
                    GenerateImageKey();
                    break;
                default:
                    return;
            }
        }
    }

    private void ImageSettingsKey()
    {
        Console.WriteLine("1 - Palette");
        Console.WriteLine("2 - Font");
        Console.WriteLine("3 - Format");
        Console.WriteLine("4 - Size");
        Console.WriteLine("5 - Back");
        var answer = Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine();
        switch (answer.KeyChar)
        {
            case '1':
                PaletteKey();
                break;
            case '2':
                FontKey();
                break;
            case '3':
                FormatKey();
                break;
            case '4':
                SizeKey();
                break;
            default:
                return;
        }
    }

    private void PaletteKey()
    {
        Console.WriteLine($"Primary color is {settings.Palette.Primary}");
        Console.WriteLine($"Background color is {settings.Palette.Background}");
        Console.WriteLine("Enter new colors as");
        Console.WriteLine("PRIMARY BACKGROUND");
        Console.WriteLine("Or pass an empty string to be brought back to menu");
        var answer = Console.ReadLine() ?? "";
        Console.WriteLine();
        var split = answer.Split(' ');

        if (split.Length != 2)
            return;

        settings.Palette.Primary = Color.FromName(split[0]);
        settings.Palette.Background = Color.FromName(split[1]);
    }

    private void FontKey()
    {
        if (!TryReadFontFamily(out var family)
            || !TryReadFontStyle(out var style))
            return;

        Console.WriteLine("Enter new size");
        Console.WriteLine("Or pass an empty string to be brought back to menu");
        var answer = Console.ReadLine() ?? "";
        Console.WriteLine();

        if (int.TryParse(answer, out var size))
            settings.Font = new Font(family, size, style);
    }

    private bool TryReadFontFamily(out FontFamily family)
    {
        Console.WriteLine("1 - Serif");
        Console.WriteLine("2 - SansSerif");
        Console.WriteLine("3 - Monospace");
        Console.WriteLine("4 - Back");
        var answer = Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine();

        family = FontFamily.GenericSerif;
        switch (answer.KeyChar)
        {
            case '1':
                family = FontFamily.GenericSerif;
                break;
            case '2':
                family = FontFamily.GenericSansSerif;
                break;
            case '3':
                family = FontFamily.GenericMonospace;
                break;
            default:
                return false;
        }

        return true;
    }

    private bool TryReadFontStyle(out FontStyle style)
    {
        Console.WriteLine("1 - Regular");
        Console.WriteLine("2 - Bold");
        Console.WriteLine("3 - Italic");
        Console.WriteLine("4 - Underline");
        Console.WriteLine("5 - Strikeout");
        Console.WriteLine("6 - Back");
        var answer = Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine();

        style = FontStyle.Regular;
        switch (answer.KeyChar)
        {
            case '1':
                style = FontStyle.Regular;
                break;
            case '2':
                style = FontStyle.Bold;
                break;
            case '3':
                style = FontStyle.Italic;
                break;
            case '4':
                style = FontStyle.Underline;
                break;
            case '5':
                style = FontStyle.Strikeout;
                break;
            default:
                return false;
        }

        return true;
    }

    private void FormatKey()
    {
        Console.WriteLine("1 - Png");
        Console.WriteLine("2 - Jpeg");
        Console.WriteLine("3 - Bmp");
        Console.WriteLine("4 - Back");
        var answer = Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine();

        switch (answer.KeyChar)
        {
            case '1':
                settings.Format = ImageFormat.Png;
                break;
            case '2':
                settings.Format = ImageFormat.Jpeg;
                break;
            case '3':
                settings.Format = ImageFormat.Bmp;
                break;
            default:
                return;
        }
    }

    private void SizeKey()
    {
        Console.WriteLine($"Image size is {settings.ImageSize}");
        Console.WriteLine("Enter new size as");
        Console.WriteLine("HEIGHT WIDTH");
        Console.WriteLine("Or pass an empty string to be brought back to menu");
        var answer = Console.ReadLine() ?? "";
        Console.WriteLine();
        var split = answer.Split(' ');

        if (split.Length != 2)
            return;

        if (int.TryParse(split[0], out var height)
            && int.TryParse(split[1], out var width))
            settings.ImageSize = new Size(height, width);
    }

    private void CloudSettingsKey()
    {
        Console.WriteLine("1 - Tag painting");
        Console.WriteLine("2 - Cloud view");
        Console.WriteLine("3 - Back");
        var answer = Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine();

        switch (answer.KeyChar)
        {
            case '1':
                TagPaintingKey();
                break;
            case '2':
                CloudViewKey();
                break;
            default:
                return;
        }
    }

    private void TagPaintingKey()
    {
        Console.WriteLine("1 - Primary");
        Console.WriteLine("2 - Frequency");
        Console.WriteLine("3 - Gradient");
        Console.WriteLine("4 - Back");
        var answer = Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine();

        switch (answer.KeyChar)
        {
            case '1':
                painter = new PrimaryTagPainter(settings);
                break;
            case '2':
                painter = new FrequencyTagPainter(settings);
                break;
            case '3':
                painter = new GradientTagPainter(settings);
                break;
            default:
                return;
        }
    }

    private void CloudViewKey()
    {
        Console.WriteLine("1 - Circle");
        Console.WriteLine("2 - Oval");
        Console.WriteLine("3 - Back");
        var answer = Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine();

        switch (answer.KeyChar)
        {
            case '1':
                spiral = new ArchimedeanSpiral(settings);
                break;
            case '2':
                spiral = new OvalSpiral(settings);
                break;
            default:
                return;
        }
    }

    private void GenerateImageKey()
    {
        var textsPath = Path.GetFullPath(@"..\..\..\texts");
        Console.WriteLine($"Texts path is {textsPath}");
        Console.WriteLine("Enter file from this folder as");
        Console.WriteLine("NAME.FORMAT");
        Console.WriteLine("Or pass an empty string to be brought back to menu");
        var answer = Console.ReadLine() ?? "";
        Console.WriteLine();
        var split = answer.Split('.');

        if (split.Length != 2)
            return;

        switch (split[1])
        {
            case "docx":
                parser = new DocParser();
                break;
            case "doc":
                parser = new DocParser();
                break;
            case "txt":
                parser = new TxtParser();
                break;
            default:
                return;
        }

        Console.WriteLine("The path for image");
        Console.WriteLine(GenerateImage(textsPath, answer));
        Console.WriteLine();
    }

    private string GenerateImage(string textsPath, string answer)
    {
        var path = Path.Combine(textsPath, answer);
        var parsed = parser.Parse(path);
        var preprocessed = applier.ApplyPreprocessors(parsed);
        var tags = creator.CreateTags(preprocessed);
        var painted = painter.Paint(tags);

        var layouter = new OvalCloudLayouter(settings, spiral);
        var cloudPainter = new TagCloudPainter(layouter, settings);
        return cloudPainter.Paint(painted);
    }
}
