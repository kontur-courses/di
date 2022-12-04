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
        //private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly Func<DragonSettings, DragonPainter> dragonPainterFactory;

        public DragonFractalAction(/*IDragonPainterFactory dragonPainterFactory*/Func<DragonSettings, DragonPainter> dragonPainterFactory)
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
            //var painter = dragonPainterFactory.CreateDragonPainter(dragonSettings);
            var painter = dragonPainterFactory(dragonSettings);
            painter.Paint();
        }

        private static DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }
    }

    //public interface IDragonPainterFactory
    //{
    //    DragonPainter CreateDragonPainter(DragonSettings settings);
    //}
}