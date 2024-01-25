using System.Drawing;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainer.DrawRectangle.Interfaces;

public interface IDraw
{
    public Bitmap CreateImage(List<Word> words);
}