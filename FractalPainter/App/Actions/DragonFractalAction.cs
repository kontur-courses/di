using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private Lazy<DragonPainter> dragonPainter;
        private readonly DragonSettings dragonSettings;
        private readonly IImageHolder imageHolder;

        public DragonFractalAction(DragonSettings dragonSettings, IImageHolder imageHolder)
        {
            this.dragonSettings = dragonSettings;
            this.imageHolder = imageHolder;
            dragonPainter = new Lazy<DragonPainter>(() => new DragonPainter(this.imageHolder, this.dragonSettings));
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            SettingsForm.For(dragonSettings).ShowDialog();
            dragonPainter.Value.Paint();
        }

    }
}