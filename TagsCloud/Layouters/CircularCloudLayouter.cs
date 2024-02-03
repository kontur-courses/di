using System.Drawing;
using TagsCloud.Distributors;
using TagsCloud.Entities;
using TagsCloud.WordFontCalculators;

namespace TagsCloud.Layouters;

public class CircularCloudLayouter : ILayouter
{
    private readonly List<Tag> tags;
    private IDistributor distributor;
    private IWordFontCalculator fontCalculator;
    public Point Center { get; private set; }
    private int leftBorder;
    private int rightBorder;
    private int topBorder;
    private int bottomBorder;

    public CircularCloudLayouter(IDistributor distributor,
        IWordFontCalculator fontCalculator,
        Point center = new Point())
    {
        this.distributor = distributor;
        this.fontCalculator = fontCalculator;
        this.Center = center;
        tags = new();
    }

    public void CreateTagCloud(Dictionary<string, int> tagsDictionary)
    {
        foreach (var tag in tagsDictionary)
        {
            var tagFont = fontCalculator.GetWordFont(tag.Key, tag.Value);
            var rectangle = new Rectangle();
            rectangle.Location = distributor.GetNextPosition();

            using (Graphics g = Graphics.FromImage(new Bitmap(1, 1)))
            {
                var sizeF = g.MeasureString(tag.Key, tagFont);
                rectangle.Size = new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
            }

            while (CheckIntersection(rectangle))
            {
                rectangle.Location = distributor.GetNextPosition();
            }

            rectangle = CompressRectangleToCenter(rectangle);
            UpdateImageSize(rectangle);
            var newtag = new Tag(rectangle, tagFont, tag.Key);
            tags.Add(newtag);
        }
    }

    public IEnumerable<Tag> GetTagsCollection()
    {
        return tags;
    }

    public Size GetImageSize()
    {
        return new Size(Math.Abs(rightBorder - leftBorder), Math.Abs(topBorder - bottomBorder));
    }

    private void UpdateImageSize(Rectangle rec)
    {
        var right = rec.X + rec.Width / 2;
        var left = rec.X - rec.Width / 2;
        var top = rec.Y + rec.Height / 2;
        var bottom = rec.Y - rec.Height / 2;

        rightBorder = right > rightBorder ? right : rightBorder;
        leftBorder = left < leftBorder ? left : leftBorder;
        topBorder = top > topBorder ? top : topBorder;
        bottomBorder = bottom < bottomBorder ? bottom : bottomBorder;
    }

    private bool CheckIntersection(Rectangle currentRectangle)
    {
        return tags.Any(rec => currentRectangle.IntersectsWith(rec.TagRectangle));
    }

    private Rectangle CompressRectangleToCenter(Rectangle rectangle)
    {
        var changes = 1;
        while (changes > 0)
        {
            rectangle = CompressByAxis(rectangle, true, out changes);
            rectangle = CompressByAxis(rectangle, false, out changes);
        }

        return rectangle;
    }

    private Rectangle CompressByAxis(Rectangle rectangle, bool isByX, out int changes)
    {
        changes = 0;
        var stepX = rectangle.X < Center.X ? 1 : -1;
        var stepY = rectangle.Y < Center.Y ? 1 : -1;

        while ((isByX && Math.Abs(rectangle.X - Center.X) > 0) ||
               (!isByX && Math.Abs(rectangle.Y - Center.Y) > 0))
        {
            var newRectangle = isByX
                ? new Rectangle(new Point(rectangle.X + stepX, rectangle.Y), rectangle.Size)
                : new Rectangle(new Point(rectangle.X, rectangle.Y + stepY), rectangle.Size);

            if (!CheckIntersection(newRectangle))
            {
                rectangle = newRectangle;
                changes++;
                continue;
            }

            break;
        }

        return rectangle;
    }
}