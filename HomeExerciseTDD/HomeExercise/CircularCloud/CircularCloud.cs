using System;
using System.Collections.Generic;
using System.Drawing;

namespace HomeExerciseTDD
{
    public class CircularCloud
    {
        private readonly List<IWord> words;
        private ICircularCloudLayouter layouter;
        private ICircularCloudProcessor cloudProcessor;
        public List<Rectangle> RectanglesInCloud { get; private set; }
        
        public CircularCloud(ICircularCloudLayouter layouter, List<IWord> words,ICircularCloudProcessor cloudProcessor)
        {
            this.words = words;
            this.layouter = layouter;
            this.cloudProcessor = cloudProcessor;
        }

        public List<Rectangle> BuildCloud()
        {
            var result = new List<Rectangle>();
            var coefficient = 1;
            
            var bitmap = new Bitmap(Int32.MaxValue, Int32.MaxValue);
            var graphics = Graphics.FromImage(bitmap);
            
            foreach (var word in words)
            {
                var size = graphics.MeasureString(word.Text, new Font(word.Font, word.Frequency*coefficient)).ToSize();
                result.Add(layouter.PutNextRectangle(size));
            }

            return result;
            throw new NotImplementedException();
        }
        
    }
}