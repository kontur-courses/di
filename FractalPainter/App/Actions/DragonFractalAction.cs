using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private IDragonPainterFactory painterFactory;
        private Func<DragonSettingsGenerator> factory;

        public DragonFractalAction(IDragonPainterFactory painterFactory, Func<DragonSettingsGenerator> factory)
        {
            this.painterFactory = painterFactory;
            this.factory = factory;
        }
        
        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = factory().Generate();
            SettingsForm.For(dragonSettings).ShowDialog();
            var painter = painterFactory.CreateDragonPainter(dragonSettings);
            painter.Paint();
        }
    }
}