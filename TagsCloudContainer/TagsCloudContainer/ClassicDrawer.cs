using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class ClassicDrawer : IDrawer
{
    private readonly Func<ILayouterAlgorithm> algorithmProvider;
    private readonly ClassicDrawerSettings classicDrawerSettings;
    private readonly Graphics graphics;

    public ClassicDrawer(ClassicDrawerSettings classicDrawerSettings, Graphics graphics,
        Func<ILayouterAlgorithm> algorithmProvider)
    {
        this.classicDrawerSettings = classicDrawerSettings;
        this.graphics = graphics;
        this.algorithmProvider = algorithmProvider;
    }

    public void DrawCloud(IReadOnlyCollection<CloudWord> cloudWords)
    {
        var tagPropertyItems = GetTagPropertyItems(cloudWords);

        Draw(tagPropertyItems);
    }

    private void Draw(
        IEnumerable<(string word, Font font, Rectangle rectangle)> tagPropertyItems)
    {
        var rectangleBorderPen = new Pen(classicDrawerSettings.RectangleBorderBrush,
            classicDrawerSettings.RectangleBorderSize);
        var numbersFont = new Font(new FontFamily(classicDrawerSettings.NumbersFamily),
            classicDrawerSettings.NumbersSize);
        var withoutBordersLocationAddition = new Size(classicDrawerSettings.RectangleBorderSize,
            classicDrawerSettings.RectangleBorderSize);
        
        graphics.FillRectangle(classicDrawerSettings.BackgroundBrush, classicDrawerSettings.ImageRectangle);
        tagPropertyItems.Foreach((item, index) => DrawTag(item.rectangle,
            withoutBordersLocationAddition,
            rectangleBorderPen,
            index + 1,
            numbersFont,
            item.word,
            item.font));
    }

    private void DrawTag(
        Rectangle layoutRectangle,
        Size withoutBordersLocationAddition,
        Pen rectangleBorderPen,
        int counter,
        Font numbersFont,
        string word,
        Font textFont)
    {
        var textStartingPoint = layoutRectangle.Location + withoutBordersLocationAddition;

        if (classicDrawerSettings.FillRectangles)
            graphics.FillRectangle(classicDrawerSettings.RectangleFillBrush, layoutRectangle);

        if (classicDrawerSettings.RectangleBorderSize != 0)
            graphics.DrawRectangle(rectangleBorderPen, layoutRectangle);

        if (classicDrawerSettings.WriteNumbers)
            graphics.DrawString(counter.ToString(), numbersFont,
                classicDrawerSettings.NumbersBrush, layoutRectangle);

        graphics.DrawString(word, textFont, classicDrawerSettings.TextBrush, textStartingPoint);
    }

    private IEnumerable<(string word, Font font, Rectangle rectangle)> GetTagPropertyItems(
        IEnumerable<CloudWord> cloudWords)
    {
        var cloudWordAndFontSizeTuples = cloudWords
            .Select(x => (word: x.Text,
                fontSize: Math.Clamp(classicDrawerSettings.MinimumTextFontSize + x.Occurrences * 3 - 3,
                    classicDrawerSettings.MinimumTextFontSize, classicDrawerSettings.MaximumTextFontSize)));

        var cloudWordAndFontTuples = cloudWordAndFontSizeTuples
            .Select(x => (x.word,
                font: new Font(new FontFamily(classicDrawerSettings.TextFontFamily), x.fontSize,
                    classicDrawerSettings.TextFontStyle)));

        var bordersSizeAddition =
            new Size(classicDrawerSettings.RectangleBorderSize, classicDrawerSettings.RectangleBorderSize) * 2;
        var cloudWordAndBlockSizeTuples = cloudWordAndFontTuples
            .Select(x => (x.word, x.font,
                size: graphics.MeasureString(x.word, x.font).ToSize() + bordersSizeAddition));

        var algorithm = algorithmProvider();

        var wordsAndRectanglesTuples = cloudWordAndBlockSizeTuples
            .Select(x => (x.word, x.font, rectangle: algorithm.PutNextRectangle(x.size)))
            .Where(x => classicDrawerSettings.ImageRectangle.Contains(x.rectangle));

        return wordsAndRectanglesTuples;
    }
}

public static class EnumerableExtensions
{
    public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
    {
        var counter = 0;
        foreach (var item in enumerable)
        {
            action(item, counter);
            counter++;
        }
    }
}