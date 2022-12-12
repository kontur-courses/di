using System.Drawing;
using System.Drawing.Imaging;
using TagCloud.Curves;
using TagCloud.Extensions;
using TagCloud.Files;
using TagCloud.Savers;
using TagCloud.Words;

namespace TagCloud.Clients;

public class Client
{
    private readonly CloudDrawer _drawer;
    private readonly CloudLayouter _layouter;
    private readonly IBitmapSaver _saver;

    public Client(CloudLayouter layouter, CloudDrawer drawer, IBitmapSaver saver)
    {
        _layouter = layouter;
        _drawer = drawer;
        _saver = saver;
    }

    public Bitmap Draw(IEnumerable<Word> words, ICurve curve, Size size, Font font, IEnumerable<Color> colors)
    {
        var wordRectangles = new List<WordRectangle>();
        var fontSize = font.Size;
        foreach (var word in words.OrderByDescending(word => word.Frequency))
        {
            font = font.ChangeSize(fontSize * word.Frequency);
            var rectangleSize = word.MeasureWord(font);
            var wordRectangle = new WordRectangle(word) { Rectangle = _layouter.PutRectangle(curve, rectangleSize) };
            wordRectangles.Add(wordRectangle);
        }
        font = font.ChangeSize(fontSize);
        var image = _drawer.CreateImage(wordRectangles, size, font, colors);
        return image;
    }
    
    public void Save(Bitmap image, IEnumerable<string> destinationPaths)
    {
        foreach (var destinationPath in destinationPaths)
            Save(image, destinationPath);
    }

    public void Save(Bitmap image, string destinationPath)
    {
        var format = Helper.GetImageFormat(destinationPath);
        _saver.Save(image, destinationPath, format);
    }
}