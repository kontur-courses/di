using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private IDragonPainterFactory painterFactory;
        private Func<DragonSettingsGenerator> settingsGenerator;
        

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public DragonFractalAction(IDragonPainterFactory painterFactory, Func<DragonSettingsGenerator> settingsGenerator)
        {
            this.painterFactory = painterFactory;
            this.settingsGenerator = settingsGenerator;
        }

        public void Perform()
        {
            var dragonSettings = settingsGenerator().Generate();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            var painter = painterFactory.CreateDragonPainter(dragonSettings);
            painter.Paint();
        }
    }
}