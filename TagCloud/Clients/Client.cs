using System.Drawing;
using System.Drawing.Imaging;
using TagCloud.Extensions;
using TagCloud.Files;
using TagCloud.Savers;
using TagCloud.Words;

namespace TagCloud.Clients;

public class Client
{
    private readonly CloudDrawer _drawer;
    private readonly IFile _fileWithWords;
    private readonly TextFormatter _formatter;
    private readonly CloudLayouter _layouter;
    private readonly IBitmapSaver _saver;

    public Client(CloudLayouter layouter, CloudDrawer drawer, TextFormatter formatter, IFile fileWithWords,
        IBitmapSaver saver)
    {
        _layouter = layouter;
        _drawer = drawer;
        _formatter = formatter;
        _fileWithWords = fileWithWords;
        _saver = saver;
    }

    public void Draw(string destinationPath, Font font)
    {
        var text = _fileWithWords.ReadAll();
        var words = _formatter.Format(text);
        var wordRectangles = new List<WordRectangle>();
        var fontSize = font.Size;
        foreach (var word in words.OrderByDescending(word => word.Amount))
        {
            font = font.ChangeSize(fontSize * word.Frequency);
            var rectangleSize = word.MeasureWord(font);
            var wordRectangle = new WordRectangle(word) { Rectangle = _layouter.PutRectangle(rectangleSize) };
            wordRectangles.Add(wordRectangle);
        }

        font = font.ChangeSize(fontSize);
        var image = _drawer.CreateImage(wordRectangles, font);
        _saver.Save(image, destinationPath, ImageFormat.Png);
    }
}