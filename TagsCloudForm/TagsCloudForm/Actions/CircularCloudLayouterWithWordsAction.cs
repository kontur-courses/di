using System;
using System.Drawing;
using TagsCloudForm.CircularCloudLayouter;
using TagsCloudForm.Common;
using TagsCloudForm.UiActions;

namespace TagsCloudForm.Actions
{
    class CircularCloudLayouterWithWordsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly Func<Point, CircularCloudLayouter.CircularCloudLayouter> circularCloudLayouterFactory;
        private readonly IWordsFrequencyParser parser;

        private readonly Func<IImageHolder,
            CircularCloudLayouterWithWordsSettings, Palette, ICircularCloudLayouter,
            IWordsFrequencyParser, CloudWithWordsPainter> painterFactory;
        public CircularCloudLayouterWithWordsAction(IImageHolder imageHolder,
             Palette palette, Func<Point, CircularCloudLayouter.CircularCloudLayouter> circularCloudLayouterFactory, IWordsFrequencyParser parser,
             Func<IImageHolder,
             CircularCloudLayouterWithWordsSettings, Palette, ICircularCloudLayouter,
             IWordsFrequencyParser, CloudWithWordsPainter> painterFactory)
        {
            this.painterFactory = painterFactory;
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.circularCloudLayouterFactory = circularCloudLayouterFactory;
            this.parser = parser;
        }
        public string Category => "CircularCloud";
        public string Name => "LayouterWithWords";
        public string Description => "Создание облака";

        public void Perform()
        {
            var settings = new CircularCloudLayouterWithWordsSettings();
            SettingsForm.For(settings).ShowDialog();
            painterFactory.Invoke(imageHolder, settings, palette, circularCloudLayouterFactory.Invoke(new Point(settings.CenterX, settings.CenterY)), parser).Paint();
        }
    }
}
