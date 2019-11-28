using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private IDragonPainterFactory factory;
        private Func<Random, DragonSettingsGenerator> createSettingsGenerator;

        public DragonFractalAction(IDragonPainterFactory dragonPainterFactory, Func<Random, DragonSettingsGenerator> settingsFactory)
        {
            factory = dragonPainterFactory;
            createSettingsGenerator = settingsFactory;
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
            factory.CreateDragonPainter(dragonSettings).Paint();
        }

        private DragonSettings CreateRandomSettings()
        {
            return createSettingsGenerator(new Random()).Generate();
        }
    }
}