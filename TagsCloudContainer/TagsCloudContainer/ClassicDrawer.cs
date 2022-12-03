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
            .ToList();


        var counter = 1;
        var rectangleBorderPen = new Pen(classicDrawerSettings.RectangleBorderBrush,
            classicDrawerSettings.RectangleBorderSize);
        var numbersFont = new Font(new FontFamily(classicDrawerSettings.NumbersFamily),
            classicDrawerSettings.NumbersSize);
        var withoutBordersLocationAddition = bordersSizeAddition / 2;
        graphics.FillRectangle(classicDrawerSettings.BackgroundBrush, classicDrawerSettings.ImageRectangle);
        foreach (var (word, font, layoutRectangle) in wordsAndRectanglesTuples)
        {
            if (!classicDrawerSettings.ImageRectangle.Contains(layoutRectangle))
                return;

            var textStartingPoint = layoutRectangle.Location + withoutBordersLocationAddition;

            if (classicDrawerSettings.FillRectangles)
                graphics.FillRectangle(classicDrawerSettings.RectangleFillBrush, layoutRectangle);

            if (!bordersSizeAddition.IsEmpty)
                graphics.DrawRectangle(rectangleBorderPen, layoutRectangle);

            if (classicDrawerSettings.WriteNumbers)
                graphics.DrawString((counter++).ToString(), numbersFont,
                    classicDrawerSettings.NumbersBrush, layoutRectangle);

            graphics.DrawString(word, font, classicDrawerSettings.TextBrush, textStartingPoint);
        }
    }
}