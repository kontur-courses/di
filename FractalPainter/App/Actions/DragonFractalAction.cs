using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private IDragonPainterFactory _factory;
        private Func<Random, DragonSettingsGenerator> _settingsFactory;
        public DragonFractalAction(IDragonPainterFactory dragonPainterFactory, Func<Random, DragonSettingsGenerator> dragonSettingsFactory)
        {
            _settingsFactory = dragonSettingsFactory;
            _factory = dragonPainterFactory;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            SettingsForm.For(dragonSettings).ShowDialog();
            _factory.CreateDragonPainter(dragonSettings).Paint();
        }

        private DragonSettings CreateRandomSettings()
        {
            return _settingsFactory(new Random()).Generate();
        }
    }
}