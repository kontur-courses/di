namespace TagsCloud.FontFinder;

public interface IFontFinder
{
    public bool HasFont(string font);
    public string?[] GetAllFonts();
}