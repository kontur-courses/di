using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm.Actions
{
    public class CircularCloudLayouterAction : IUiAction
    {
        private CloudPainterFactory PainterFactory;
        private readonly IImageHolder imageHolder;
        //private readonly CircularCloudLayouterSettings settings;
        private readonly Palette palette;
        private readonly Func<Point, CircularCloudLayouter> CircularCloudLayouterFactory;
        public CircularCloudLayouterAction(CloudPainterFactory cloudPainterFactory, IImageHolder imageHolder,
            CircularCloudLayouterSettings settings, Palette palette, Func<Point, CircularCloudLayouter> circularCloudLayouterFactory)
        {
            this.PainterFactory = cloudPainterFactory;
            this.imageHolder = imageHolder;
            //this.settings = settings;
            this.palette = palette;
            this.CircularCloudLayouterFactory = circularCloudLayouterFactory;
        }
        public string Category => "CircularCloud";
        public string Name => "Cloud";
        public string Description => "Создание облака";

        public void Perform()
        {
            var settings = new CircularCloudLayouterSettings();
            //var dragonSettings = CreateRandomSettings();
            // редактируем настройки:
            //SettingsForm.For(dragonSettings).ShowDialog();
            SettingsForm.For(settings).ShowDialog();
            PainterFactory.Create(imageHolder, settings, palette, CircularCloudLayouterFactory.Invoke(new Point(settings.CenterX, settings.CenterY))).Paint();
            // создаём painter с такими настройками
        }
    }
}
