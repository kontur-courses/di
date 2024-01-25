using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.App.Extensions;
using TagsCloudContainer.App.Interfaces;
using TagsCloudContainer.DrawRectangle.Interfaces;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainer.App;

public class ConsoleApp : IApp
{
    private readonly FileReaderFactory _readerFactory;
    private readonly Settings _settings;
    private readonly WordProcessor _processor;
    private readonly IDraw _draw;

    public ConsoleApp(FileReaderFactory readerFactory, Settings settings, WordProcessor processor,
        IDraw draw)
    {
        _readerFactory = readerFactory;
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _processor = processor;
        _draw = draw;
    }
    
    public void Run()
    {
        var text = GetText(_settings.File);
        var boringText = GetText(_settings.BoringWordsFileName);
        var words = _processor.ProcessWords(text, boringText);
        var bitmap = _draw.CreateImage(words);
        var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        var rnd = new Random();
        bitmap.Save(projectDirectory + "\\Images", $"Rectangles{rnd.Next(1, 1000)}", ImageFormat.Png);
    }

    private string GetText(string filename)
    {
        if (!File.Exists(filename))
            throw new ArgumentException($"Файл не найден {filename}");
        
        return _readerFactory.GetReader(filename).GetTextFromFile(filename);
    }
}