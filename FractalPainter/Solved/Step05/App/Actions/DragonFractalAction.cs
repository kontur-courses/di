using System;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step05.App.Fractals;
using FractalPainting.Solved.Step05.Infrastructure.UiActions;

namespace FractalPainting.Solved.Step05.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory dragonPainterFactory;

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
            dragonPainterFactory.Create(dragonSettings).Paint();
        }

        private static DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }
    }
}