using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory factory;
        private readonly Func<DragonSettingsGenerator> createSettingsGenerator;

        public DragonFractalAction(Func<DragonSettingsGenerator> createSettingsGenerator, IDragonPainterFactory factory)
        {
            this.factory = factory;
            this.createSettingsGenerator = createSettingsGenerator;
        }

        #region IUiAction

        public string Category => "Фракталы";
        public int Order => 0;
        public int CategoryOrder => 1;
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = createSettingsGenerator().Generate();
            SettingsForm.For(dragonSettings).ShowDialog();
            factory.CreateDragonPainter(dragonSettings).Paint();
        }

        #endregion
    }
}