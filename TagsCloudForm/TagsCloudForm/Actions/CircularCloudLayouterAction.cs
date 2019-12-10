using System;
using System.Drawing;
using CircularCloudLayouter;
using TagsCloudForm.CircularCloudLayouter;
using TagsCloudForm.Common;
using TagsCloudForm.UiActions;

namespace TagsCloudForm.Actions
{
    public class CircularCloudLayouterAction : IUiAction
    {
        private readonly CloudPainterFactory factory;
        public CircularCloudLayouterAction(CloudPainterFactory factory)
        {
            this.factory = factory;
        }
        public string Category => "CircularCloud";
        public string Name => "Layouter";
        public string Description => "Создание облака";

        public void Perform()
        {
            var settings = new CircularCloudLayouterSettings();
            SettingsForm.For(settings).ShowDialog();
            factory.Create(settings).Paint();
        }
    }
}
