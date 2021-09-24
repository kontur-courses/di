using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly Func<Random, DragonSettingsGenerator> dragonSettingsGeneratorFactory;
        private readonly IImageHolder imageHolder;

        public DragonFractalAction(IImageHolder imageHolder, IDragonPainterFactory dragonPainterFactory, Func<Random, DragonSettingsGenerator> dragonSettingsGeneratorFactory)
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
            var dragonSettingGenerator = dragonSettingsGeneratorFactory(new Random());
            var dragonSettings = dragonSettingGenerator.Generate();
            SettingsForm.For(dragonSettings).ShowDialog();
            var painter = dragonPainterFactory.CreateDragonPainter(imageHolder, dragonSettings);
            painter.Paint();
        }

        public int Order => 2;
    }
    
    public interface IDragonPainterFactory
    {
        DragonPainter CreateDragonPainter(IImageHolder imageHolder, DragonSettings settings);
    }
}