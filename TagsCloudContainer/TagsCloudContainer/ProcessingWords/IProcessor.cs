using System.Drawing;

namespace TagsCloudContainer.ProcessingWords
{
    public interface IProcessor
    {
        Bitmap Run(string pathToFile, Color colorBackground,
            string famyilyNameFont, Brush brushText, StringFormat stringFormatText, Size size);
    }
}