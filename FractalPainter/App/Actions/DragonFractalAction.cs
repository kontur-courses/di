using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private IImageHolder imageHolder;
        private IDragonPainterFactory dragonPainterFactory;
        private Func<DragonSettingsGenerator> dragonSettingsGeneratorFactory;

        public DragonFractalAction(IImageHolder imageHolder, IDragonPainterFactory dragonPainterFactory, Func<DragonSettingsGenerator> dragonSettingsGeneratorFactory)
        {
            this.imageHolder = imageHolder;
            this.dragonPainterFactory = dragonPainterFactory;
            this.dragonSettingsGeneratorFactory = dragonSettingsGeneratorFactory;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = dragonSettingsGeneratorFactory().Generate();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            dragonPainterFactory.Create(imageHolder, dragonSettings).Paint();
        }
    }
}