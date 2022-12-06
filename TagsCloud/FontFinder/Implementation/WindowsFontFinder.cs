namespace TagsCloud.FontFinder.Implementation;

public class WindowsFontFinder : IFontFinder
{
    private const string PathToFonts = @"C:\Windows\Fonts\";
    
    public bool HasFont(string font) => File.Exists(PathToFonts + font);

    public string?[] GetAllFonts() =>
        Directory.GetFiles(PathToFonts).Select(Path.GetFileNameWithoutExtension).ToArray();
}