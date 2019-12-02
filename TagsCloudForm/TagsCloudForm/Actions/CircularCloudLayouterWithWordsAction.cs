using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm.Actions
{
    class CircularCloudLayouterWithWordsAction : IUiAction
    {
        private CloudWithWordsPainterFactory PainterFactory;
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly Func<Point, CircularCloudLayouter> CircularCloudLayouterFactory;
        private readonly Dictionary<string, int> words;
        public CircularCloudLayouterWithWordsAction(CloudWithWordsPainterFactory cloudPainterFactory, IImageHolder imageHolder,
             Palette palette, Func<Point, CircularCloudLayouter> circularCloudLayouterFactory, Dictionary<string, int> words)
        {
            this.PainterFactory = cloudPainterFactory;
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.CircularCloudLayouterFactory = circularCloudLayouterFactory;
            this.words = words;
        }
        public string Category => "CircularCloud";
        public string Name => "LayouterWithWords";
        public string Description => "Создание облака";

        public void Perform()
        {
            var settings = new CircularCloudLayouterSettings();
            SettingsForm.For(settings).ShowDialog();
            PainterFactory.Create(imageHolder, settings, palette, CircularCloudLayouterFactory.Invoke(new Point(settings.CenterX, settings.CenterY)), words).Paint();
        }
    }
}
