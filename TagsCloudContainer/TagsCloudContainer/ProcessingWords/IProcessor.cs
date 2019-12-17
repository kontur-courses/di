using System.Drawing;

namespace TagsCloudContainer.ProcessingWords
{
    public interface IProcessor
    {
        void Run(string pathToFile, string pathSave, Color colorBackground,
            string famyilyNameFont, Brush brushText, StringFormat stringFormatText, Size size);
    }
}