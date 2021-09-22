using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly Func<DragonSettings> dragonSettingsFactory;
        private readonly IImageHolder imageHolder;

        public DragonFractalAction(IImageHolder imageHolder, IDragonPainterFactory dragonPainterFactory, Func<DragonSettings> dragonSettingsFactory)
        {
            this.imageHolder = imageHolder;
            this.dragonPainterFactory = dragonPainterFactory;
            this.dragonSettingsFactory = dragonSettingsFactory;
        }
        
        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = dragonSettingsFactory.Invoke();
            SettingsForm.For(dragonSettings).ShowDialog();
            var painter = dragonPainterFactory.CreateDragonPainter(imageHolder, dragonSettings);
            painter.Paint();
        }
    }
    
    public interface IDragonPainterFactory
    {
        DragonPainter CreateDragonPainter(IImageHolder imageHolder, DragonSettings settings);
    }
}