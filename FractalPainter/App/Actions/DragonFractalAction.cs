using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory dragonFactory;
        private readonly Func<Random, DragonSettingsGenerator> settingsFactory;
        
        public DragonFractalAction(IDragonPainterFactory dragonFactory,Func<Random, DragonSettingsGenerator> settingsFactory)
        {
            this.dragonFactory = dragonFactory;
            this.settingsFactory = settingsFactory;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = settingsFactory(new Random()).Generate();
            SettingsForm.For(dragonSettings).ShowDialog();
            dragonFactory.Creatre(dragonSettings).Paint();
        }
    }
}