using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly IImageHolder imageHolder;

        public DragonFractalAction(IImageHolder imageHolder, IDragonPainterFactory dragonPainterFactory)
        {
            this.imageHolder = imageHolder;
            this.dragonPainterFactory = dragonPainterFactory;
        }
        
        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            SettingsForm.For(dragonSettings).ShowDialog();
            var painter = dragonPainterFactory.CreateDragonPainter(imageHolder, dragonSettings);
            painter.Paint();
        }

        private static DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }
    }
    
    public interface IDragonPainterFactory
    {
        DragonPainter CreateDragonPainter(IImageHolder imageHolder, DragonSettings settings);
    }
}