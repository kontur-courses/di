using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private IImageHolder imageHolder;
        private IDragonPainterFactory dragonPainterFactory;
        private Func<DragonSettingsGenerator> dragonSettingsGenerator;

        public DragonFractalAction(IImageHolder imageHolder, IDragonPainterFactory dragonPainterFactory, Func<DragonSettingsGenerator> dragonSettingsGenerator)
        {
            this.imageHolder = imageHolder;
            this.dragonPainterFactory = dragonPainterFactory;
            this.dragonSettingsGenerator = dragonSettingsGenerator;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = dragonSettingsGenerator().Generate();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            dragonPainterFactory.CreateDragonPainter(dragonSettings).Paint();
        }
    }
}