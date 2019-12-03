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
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly Func<Point, CircularCloudLayouter> CircularCloudLayouterFactory;
        private readonly IWordsFrequencyParser parser;

        private readonly Func<IImageHolder,
            CircularCloudLayouterWithWordsSettings, Palette, ICircularCloudLayouter,
            IWordsFrequencyParser, CloudWithWordsPainter> PainterFactory;
        public CircularCloudLayouterWithWordsAction(IImageHolder imageHolder,
             Palette palette, Func<Point, CircularCloudLayouter> circularCloudLayouterFactory, IWordsFrequencyParser parser,
             Func<IImageHolder,
             CircularCloudLayouterWithWordsSettings, Palette, ICircularCloudLayouter,
             IWordsFrequencyParser, CloudWithWordsPainter> painterFactory)
        {
            this.PainterFactory = painterFactory;
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.CircularCloudLayouterFactory = circularCloudLayouterFactory;
            this.parser = parser;
        }
        public string Category => "CircularCloud";
        public string Name => "LayouterWithWords";
        public string Description => "Создание облака";

        public void Perform()
        {
            var settings = new CircularCloudLayouterWithWordsSettings();
            SettingsForm.For(settings).ShowDialog();
            PainterFactory.Invoke(imageHolder, settings, palette, CircularCloudLayouterFactory.Invoke(new Point(settings.CenterX, settings.CenterY)), parser).Paint();
        }
    }
}
