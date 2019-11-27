using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly Func<Random, DragonSettingsGenerator> createDragonSettings;

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public DragonFractalAction(IDragonPainterFactory dragonPainterFactory, 
            Func<Random, DragonSettingsGenerator> createDragonSettings)
        {
            this.dragonPainterFactory = dragonPainterFactory;
            this.createDragonSettings = createDragonSettings;
        }
        
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
            return createDragonSettings(new Random()).Generate();
        }
    }

    public interface IDragonPainterFactory
    {
        DragonPainter Create(DragonSettings dragonSettings);
    }
}