using System.Drawing;
using TagsCloud2.FrequencyCompiler;
using TagsCloud2.TagsCloudMaker.BitmapMaker;
using TagsCloud2.TagsCloudMaker.SizeDefiner;

namespace TagsCloud2.TagsCloudMaker;

public class TagsCloudMaker : ITagsCloudMaker
{
    public Bitmap MakeTagsCloud(List<WordFrequency> frequency, 
        string fontFamilyName, int bigFontSize, Brush color, Size bitmapSize,
        IBitmapTagsCloudMaker bitmapTagsCloudMaker, ISizeDefiner sizeDefiner, bool withVertical)
    {
        var listLength = frequency.Count;
        var bigFontAmount = listLength / 5;
        var middleFontAmount = listLength / 5 * 2;
        var stringAndFontSize = new Dictionary<string, int>();
        DefineFontSize(frequency, bigFontSize, bigFontAmount, stringAndFontSize, middleFontAmount, listLength);
        var stringOptions = 
            sizeDefiner.DefineStringSizeAndOrientation(stringAndFontSize, withVertical, fontFamilyName);
        var tagsCloudBitmap = bitmapTagsCloudMaker.MakeBitmap(bitmapSize, stringOptions, fontFamilyName, color);
        return tagsCloudBitmap;
    }

    private static void DefineFontSize(List<WordFrequency> frequency, int bigFontSize, int bigFontAmount, Dictionary<string, int> stringAndFontSize,
        int middleFontAmount, int listLength)
    {
        for (var i = 0; i < bigFontAmount; i++)
        {
            stringAndFontSize.Add(frequency[i].Word, bigFontSize);
        }

        for (var i = bigFontAmount; i < bigFontAmount + middleFontAmount; i++)
        {
            stringAndFontSize.Add(frequency[i].Word, bigFontSize/5*4);
        }

        for (var i = bigFontAmount + middleFontAmount; i < listLength; i++)
        {
            stringAndFontSize.Add(frequency[i].Word, bigFontSize/5*3);
        }
    }
}