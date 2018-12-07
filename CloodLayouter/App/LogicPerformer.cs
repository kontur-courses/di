using System.Collections.Generic;
using System.Linq;
using CloodLayouter.Infrastructer;
using CloodLayouter.Infrastructer.Extensions;
using CloudLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class LogicPerformer
    {
        private readonly IStreamReader reader;
        private readonly IWordSelector selector;
        private readonly IConverter converter;
        private readonly ICloudLayouter cloudLayouter;
        private readonly IGraphicsHolder graphicsHolder;
        
        public LogicPerformer(IStreamReader streamReader, IWordSelector wordSelector, IConverter converter,
            ICloudLayouter cloudLayouter, IGraphicsHolder graphicsHolder)
        {
            this.reader = streamReader;
            this.selector = wordSelector;
            this.converter = converter;
            this.cloudLayouter = cloudLayouter;
            this.graphicsHolder = graphicsHolder;
        }

        public void Perfom(string filename)
        {
            var words = reader.Read(filename);
            var selectedWords = selector.Select(words);
            var tags = converter.Convert(selectedWords);
            foreach (var tag in tags)
            {
                var size = graphicsHolder.Measure(tag).ToSizeI();
                var rectangle = cloudLayouter.PutNextRectangle(size);
                graphicsHolder.Draw(rectangle,tag);
            }
            graphicsHolder.Save();
        }
    }
}