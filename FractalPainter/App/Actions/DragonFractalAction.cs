using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    // public interface IDragonPainterFactory
    // {
    //     DragonPainter CreateDragonPainter(DragonSettings settings);
    // }

    public class DragonFractalAction : IUiAction
    {
        //private readonly IDragonPainterFactory factory;
        private readonly Func<DragonSettings, DragonPainter> createPainter;

        public DragonFractalAction(Func<DragonSettings, DragonPainter> createPainter/*, IDragonPainterFactory factory*/)
        {
            //this.factory = factory;
            this.createPainter = createPainter;
        }

        #region IUiAction

        public string Category => "Фракталы";
        public int CategoryOrder => 1;
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            SettingsForm.For(dragonSettings).ShowDialog();
            
            //factory.CreateDragonPainter(dragonSettings).Paint();
            createPainter(dragonSettings).Paint();
        }

        #endregion

        private static DragonSettings CreateRandomSettings() => 
            new DragonSettingsGenerator(new Random()).Generate();
    }
}