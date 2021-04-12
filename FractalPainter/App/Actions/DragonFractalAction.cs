using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly Func<DragonSettingsGenerator> dragonSettingsGeneratorFactory;

        public DragonFractalAction(IImageHolder imageHolder,
            IDragonPainterFactory dragonPainterFactory,
            Func<DragonSettingsGenerator> dragonSettingsGeneratorFactory)
        {
            this.imageHolder = imageHolder;
            this.dragonPainterFactory = dragonPainterFactory;
            this.dragonSettingsGeneratorFactory = dragonSettingsGeneratorFactory;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";
        public int Order => 2; // TODO лучше выделить категорию в отдельный класс

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            dragonPainterFactory.CreateDragonPainter(imageHolder, dragonSettings).Paint();
        }

        private DragonSettings CreateRandomSettings()
        {
            return dragonSettingsGeneratorFactory().Generate();
        }
    }
}