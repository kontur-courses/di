using TagCloud.Domain.Layouter.Interfaces;
using TagCloud.Domain.Settings;
using TagCloud.Domain.Visualizer.Interfaces;
using TagCloud.Domain.WordEntities;
using TagCloud.Domain.WordProcessing.Interfaces;
using TagCloud.Utils.Extensions;

namespace TagCloud.Domain.Visualizer.Visualizers;

public class TagCloudVisualizer : IVisualizer
{
    private readonly IWordProcessor wordProcessor;
    private readonly ICloudLayouter cloudLayouter;
    private readonly TagCloudSettings settings;

    public TagCloudVisualizer(
        IWordProcessor wordProcessor,
        ICloudLayouter cloudLayouter,
        TagCloudSettings settings)
    {
        this.wordProcessor = wordProcessor;
        this.cloudLayouter = cloudLayouter;
        this.settings = settings;
    }

    public Image Visualize(IEnumerable<string> words)
    {
        var clearWords = wordProcessor.GetClearWordsWithCount(words);
        var bitmap = new Bitmap(settings.LayoutSettings.Dimensions.Width,
            settings.LayoutSettings.Dimensions.Height);
        using var graphics = Graphics.FromImage(bitmap);
        
        var wordsWithInfo = GetWordsWithInfo(clearWords, graphics);
        
        FillBg(graphics);
        DrawWordsWithInfo(wordsWithInfo, graphics);
        
        return bitmap;
    }

    private WordToVisualize[] GetWordsWithInfo(WordsWithCount clearWords, Graphics graphics)
    {
        var wordsWithInfo = new WordToVisualize[clearWords.Words.Length]; 

        for (var i = 0; i < clearWords.Words.Length; i++)
        {
            var word = clearWords.Words[i];
            var percent = 1 + (word.Count - clearWords.MinCount) / (float) clearWords.CountWindow;
            
            var font = new Font(
                settings.VisualizerSettings.Font.FontFamily,
                settings.VisualizerSettings.Font.Size * percent);
            var size = graphics.MeasureString(word.Text, font);
            var rect = cloudLayouter.PutNextRectangle(new Size(
                (int)Math.Ceiling(size.Width),
                (int)Math.Ceiling(size.Height)));
            
            wordsWithInfo[i] = new WordToVisualize(word, rect, font);
        }

        return wordsWithInfo;
    }

    private void FillBg(Graphics graphics)
    {
        using var brush = new SolidBrush(settings.VisualizerSettings.BgColor);
        graphics.FillRectangle(brush, graphics.VisibleClipBounds);
    }

    private void DrawWordsWithInfo(WordToVisualize[] words, Graphics graphics)
    {
        using var brush = new SolidBrush(settings.VisualizerSettings.Color);
        foreach (var word in words) 
            graphics.DrawString(word.Text, word.Font, brush, word.Rectangle.ToRectangleF());
    }

}