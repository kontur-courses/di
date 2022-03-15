using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly Func<DragonSettings> dragonSettingsFactory;
        public DragonFractalAction(IDragonPainterFactory dragonPainterFactory, Func<DragonSettings> dragonSettingsFactory)
        {
            this.dragonPainterFactory = dragonPainterFactory;
            this.dragonSettingsFactory = dragonSettingsFactory;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = this.dragonSettingsFactory();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            var dragonPainter = dragonPainterFactory.CreateDragonPainter(dragonSettings);
            dragonPainter.Paint();
        }
    }
    public interface IDragonPainterFactory
    {
        DragonPainter CreateDragonPainter(DragonSettings settings);
    }

}