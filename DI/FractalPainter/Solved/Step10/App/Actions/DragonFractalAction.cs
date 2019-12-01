using System;
using FractalPainting.Solved.Step10.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step10.Infrastructure.UiActions;

namespace FractalPainting.Solved.Step10.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly Func<Random, DragonSettingsGenerator> createDragonSettingsGenerator;

        public DragonFractalAction(IDragonPainterFactory dragonPainterFactory,
            Func<Random, DragonSettingsGenerator> createDragonSettingsGenerator)
        {
            this.dragonPainterFactory = dragonPainterFactory;
            this.createDragonSettingsGenerator = createDragonSettingsGenerator;
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

        private DragonSettings CreateRandomSettings()
        {
            return createDragonSettingsGenerator(new Random()).Generate();
        }
    }
}