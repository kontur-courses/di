using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly Func<DragonSettingsGenerator> dragonSettingGeneratorFactory;
        private readonly Func<DragonSettings, DragonPainter> dragonPainterFactory;

        public DragonFractalAction(Func<DragonSettingsGenerator> dragonSettingGeneratorFactory,
            Func<DragonSettings, DragonPainter> dragonPainterFactory)
        {
            this.dragonSettingGeneratorFactory = dragonSettingGeneratorFactory;
            this.dragonPainterFactory = dragonPainterFactory;
        }

        public string CategoryName => "Фракталы";
        public Category Category => Category.Fractals;

        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = dragonSettingGeneratorFactory().Generate();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            dragonPainterFactory(dragonSettings).Paint();
        }
    }
}