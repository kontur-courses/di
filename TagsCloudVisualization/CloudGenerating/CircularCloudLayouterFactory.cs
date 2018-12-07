using System;

namespace TagsCloudVisualization.CloudGenerating
{
    public class CircularCloudLayouterFactory : ILayouterFactory
    {
        private readonly ISpiralGeneratorFactory spiralGeneratorFactory;

        public CircularCloudLayouterFactory(ISpiralGeneratorFactory spiralGeneratorFactory)
        {
            this.spiralGeneratorFactory = spiralGeneratorFactory;
        }

        public ILayouter Create()
        {
            return new CircularCloudLayouter(spiralGeneratorFactory.Create());
        }
    }
}
