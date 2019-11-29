using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using System;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private IDragonPainterFactory dragonPainterFactory;

        public DragonFractalAction(IDragonPainterFactory dragonPainterFactory)
        {
            this.dragonPainterFactory = dragonPainterFactory;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();

            // создаём painter с такими настройками
            dragonPainterFactory.CreateDragonPainter(dragonSettings).Paint();
        }

        private static DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }
    }
}