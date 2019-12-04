using System;
using System.Drawing;
using TagsCloudForm.CircularCloudLayouter;
using TagsCloudForm.Common;
using TagsCloudForm.UiActions;

namespace TagsCloudForm.Actions
{
    public class CircularCloudLayouterAction : IUiAction
    {
        private readonly Func<IImageHolder,
            CircularCloudLayouterSettings, 
            Palette, ICircularCloudLayouter, CloudPainter> painterFactory;
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly Func<Point, CircularCloudLayouter.CircularCloudLayouter> circularCloudLayouterFactory;
        public CircularCloudLayouterAction(Func<IImageHolder,
                CircularCloudLayouterSettings,
                Palette, ICircularCloudLayouter, CloudPainter> painterFactory, IImageHolder imageHolder,
            Palette palette, Func<Point, CircularCloudLayouter.CircularCloudLayouter> circularCloudLayouterFactory)
        {
            this.painterFactory = painterFactory;
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.circularCloudLayouterFactory = circularCloudLayouterFactory;
        }
        public string Category => "CircularCloud";
        public string Name => "Layouter";
        public string Description => "Создание облака";

        public void Perform()
        {
            var settings = new CircularCloudLayouterSettings();
            SettingsForm.For(settings).ShowDialog();
            var layouter = circularCloudLayouterFactory.Invoke(new Point(settings.CenterX, settings.CenterY));
            layouter.SetCompression(settings.XCompression, settings.YCompression);
            painterFactory.Invoke(imageHolder, settings, palette, layouter).Paint();
        }
    }
}
