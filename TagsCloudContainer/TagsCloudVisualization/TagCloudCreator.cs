using System.Drawing;
using TagsCloudVisualization.TextHandlers;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private readonly ITextHandler handler;
        private readonly IVisualizer visualizer;

        public TagCloudCreator(IVisualizer visualizer, ITextHandler handler)
        {
            this.visualizer = visualizer;
            this.handler = handler;
        }
        
        public Bitmap CreateFromFile(string filepath)
        {
            var text = handler.Handle(filepath);

            return visualizer.Visualize(text);
        }
    }
}