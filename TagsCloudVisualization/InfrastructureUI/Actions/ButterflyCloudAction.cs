using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure.Algorithm.Curves;
using TagsCloudVisualization.InfrastructureUI.Painters;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI.Actions
{
    public class ButterflyCloudAction : IUiAction
    {
        private readonly Func<DefinerSize, ICurve, CloudPainter> factory;
        private readonly IImageHolder imageHolder;
        private readonly FontSettings settings;

        public ButterflyCloudAction(Func<DefinerSize, ICurve, CloudPainter> factoryPainter,
            IImageHolder imageHolder,
            FontSettings settings)
        {
            this.settings = settings;
            factory = factoryPainter;
            this.imageHolder = imageHolder;
        }

        public string Category => "Виды облаков";
        public string Name => "Бабочка";
        public string Description => "";
        public void Perform()
        {
            var size = imageHolder.GetImageSize();
            var definerSize = new DefinerSize(settings);
            factory(definerSize, new Butterfly(new Point(size.Width / 2, size.Height /2))).Paint();
        }
    }
}