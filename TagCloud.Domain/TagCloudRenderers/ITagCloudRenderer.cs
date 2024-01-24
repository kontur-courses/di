using System.Drawing;

public interface ITagCloudRenderer
{
    Bitmap Render(WordLayout[] wordLayouts);

    Size GetStringSize(string str, int fontSize);
}
