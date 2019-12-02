﻿using System;
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
        private readonly Palette palette;
        private readonly Func<Point, CircularCloudLayouter> CircularCloudLayouterFactory;
        public CircularCloudLayouterAction(CloudPainterFactory cloudPainterFactory, IImageHolder imageHolder,
            Palette palette, Func<Point, CircularCloudLayouter> circularCloudLayouterFactory)
        {
            this.PainterFactory = cloudPainterFactory;
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.CircularCloudLayouterFactory = circularCloudLayouterFactory;
        }
        public string Category => "CircularCloud";
        public string Name => "Layouter";
        public string Description => "Создание облака";

        public void Perform()
        {
            var settings = new CircularCloudLayouterSettings();
            SettingsForm.For(settings).ShowDialog();
            PainterFactory.Create(imageHolder, settings, palette, CircularCloudLayouterFactory.Invoke(new Point(settings.CenterX, settings.CenterY))).Paint();
        }
    }
}
