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
        private readonly Func<DragonSettingsGenerator> dragonSettingsGenerator;

        public DragonFractalAction(IImageHolder imageHolder, IDragonPainterFactory dragonPainterFactory, Func<DragonSettingsGenerator> dragonSettingsGenerator)
        {
            this.imageHolder = imageHolder;
            this.dragonPainterFactory = dragonPainterFactory;
            this.dragonSettingsGenerator = dragonSettingsGenerator;
        }


        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";
        public int Order => 2;

        public void Perform()
        {
            var dragonSettings = dragonSettingsGenerator().Generate();
            SettingsForm.For(dragonSettings).ShowDialog();

            var dragonPainter = dragonPainterFactory.CreateDragonPainter(imageHolder, dragonSettings);
            dragonPainter.Paint();
        }
    }
}