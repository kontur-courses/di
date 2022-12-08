using System;
using System.Drawing;
using TagsCloudVisualization.Infrastructure.Algorithm.Curves;
using TagsCloudVisualization.Painters;

namespace TagsCloudVisualization.InfrastructureUI
{
    public class ButterflyCloudAction : IUiAction
    {
        private readonly Func<DefinerSize, ICurve, CloudPainter> factory;
        private readonly IImageHolder imageHolder;

        public ButterflyCloudAction(Func<DefinerSize, ICurve, CloudPainter> factoryPainter, IImageHolder imageHolder)
        {
            factory = factoryPainter;
            this.imageHolder = imageHolder;
        }

        public string Category => "Виды облаков";
        public string Name => "Бабочка";
        public string Description => "";
        public void Perform()
        {
            var size = imageHolder.GetImageSize();
            var settings = new FontSettings();
            SettingsForm.For(settings).ShowDialog();
            var definerSize = new DefinerSize(settings);
            factory(definerSize, new Butterfly(new Point(size.Width / 2, size.Height /2))).Paint();
        }
    }
}