using System.Drawing;


namespace TagsCloudContainer
{
    internal interface IDrawSettings<T>
    {
        Size GetImageSize();
        string GetFontName();
        SolidBrush GetBrush(IItemToDraw<T> item);
        string GetFullFileName();
    }
}