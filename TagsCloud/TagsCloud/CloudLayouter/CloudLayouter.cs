using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.CloudLayouter
{
    public class CloudLayouter
    {
        public readonly Point Center;

        public HashSet<Word> Words { get; private set; } = new HashSet<Word>();

        private readonly IPointGenerator pointGenerator;

        public CloudLayouter(Point center, IPointGenerator pointGenerator)
        {
            Center = center;
            this.pointGenerator = pointGenerator;
        }

        public Word PutNextWord(string name, Size wordSize)
        {
            ThrowExceptionOnWrongSize(wordSize);

            var posRectangle = new Rectangle(ChooseWordLocation(wordSize), wordSize);
            var word = new Word(posRectangle, name);
            Words.Add(word);
            return word;
        }

        private Point ChooseWordLocation(Size rectSize)
        {
            var resultCenter = new Point();
            foreach (var point in pointGenerator.GeneratePoints(Center))
                if (!DefineRectangle(point, rectSize).IntersectsWithAny(Words))
                {
                    resultCenter = point;
                    break;
                }

            return CountLocation(resultCenter, rectSize);
        }

        private Rectangle DefineRectangle(Point rectCenter, Size rectSize)
        {
            return new Rectangle(CountLocation(rectCenter, rectSize), rectSize);
        }

        private Point CountLocation(Point rectCenter, Size rectSize)
        {
            var resultX = rectCenter.X - rectSize.Width / 2;
            var resultY = rectCenter.Y - rectSize.Height / 2;
            return new Point(resultX, resultY);
        }

        private void ThrowExceptionOnWrongSize(Size rectSize)
        {
            if (rectSize.Height <= 0 || rectSize.Width <= 0)
                throw new ArgumentException(rectSize.ToString());
        }
    }
}
