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
        private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly Func<DragonSettingsGenerator> generatorFactory;
        public DragonFractalAction(IDragonPainterFactory dragonPainterFactory, Func<DragonSettingsGenerator> generatorFactory)
        {
            this.dragonPainterFactory = dragonPainterFactory;
            this.generatorFactory = generatorFactory;
        }
        
        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = generatorFactory().Generate();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            var painter = dragonPainterFactory.CreateDragonPainter(dragonSettings);
            painter.Paint();
        }
        
    }
}