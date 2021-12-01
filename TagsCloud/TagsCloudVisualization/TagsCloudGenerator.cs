using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagsCloudGenerator
    {
        private readonly TagsCloudDrawer _drawer;
        private readonly CircularCloudLayouter _layouter;
        private readonly int _rectanglesCount;
        private readonly Func<Size> _sizeFactory;

        public TagsCloudGenerator(
            int rectanglesCount,
            CircularCloudLayouter layouter,
            Func<Size> sizeFactory,
            TagsCloudDrawer drawer)
        {
            if (rectanglesCount <= 0)
                throw new ArgumentException(
                    $"{nameof(rectanglesCount)} should be positive");
            _rectanglesCount = rectanglesCount;
            _layouter = layouter ?? throw new ArgumentNullException(nameof(layouter));
            _sizeFactory = sizeFactory ?? throw new ArgumentNullException(nameof(sizeFactory));
            _drawer = drawer ?? throw new ArgumentNullException(nameof(drawer));
        }

        public void Generate(Bitmap bitmap)
        {
            if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));
            _drawer.Draw(bitmap, GenerateRectangles());
        }

        private IEnumerable<Rectangle> GenerateRectangles()
        {
            return Enumerable.Range(0, _rectanglesCount)
                .Select(x => _layouter.PutNextRectangle(_sizeFactory()));
        }
    }
}