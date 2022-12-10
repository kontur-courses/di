using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure.Algorithm.Curves;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI.Actions
{
    public class CircleCloudAction : IUiAction
    {
        private readonly Func<DefinerSize, ICurve, CloudPainter> factory;
        private readonly IImageHolder imageHolder;
        private readonly FontSettings settings;

        public CircleCloudAction(Func<DefinerSize, ICurve, CloudPainter> factoryPainter,
            IImageHolder imageHolder, FontSettings settings)
        {
            this.settings = settings;
            factory = factoryPainter;
            this.imageHolder = imageHolder;
        }

        public string Category => "Виды облаков";
        public string Name => "Окружность";
        public string Description => "";
        public void Perform()
        {
            var size = imageHolder.GetImageSize();

            var definerSize = new DefinerSize(settings);
            factory(definerSize,
                new Spiral(1,new Point(size.Width/ 2, size.Height /2)))
                .Paint();
        }
    }
}