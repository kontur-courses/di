using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        //TODO:5.1
        /* private readonly IDragonPainterFactory painterFactory;
         public DragonFractalAction(IDragonPainterFactory painterFactory)
         {
             this.painterFactory=painterFactory;
         }*/

        //TODO:5.2
        private readonly Func<DragonSettings,DragonPainter> painterFactory;
        public DragonFractalAction(Func<DragonSettings,DragonPainter> painterFactory)
         {
             this.painterFactory=painterFactory;
         }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var settings = CreateRandomSettings();
            SettingsForm.For(settings).ShowDialog();
            painterFactory(settings).Paint();
        }

        private DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }

    }
}