using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction /*INeed<IImageHolder>*/
    {
        //private IImageHolder imageHolder;
        private IDragonFactory dragonFactory;
        private readonly Func<Random, DragonSettingsGenerator> settingsFactory;

        //public void SetDependency(IImageHolder dependency)
        //{
        //    imageHolder = dependency;
        //}

        public DragonFractalAction(IDragonFactory dragonFactory, Func<Random, DragonSettingsGenerator> dragonSettingsFactory)
        {
            this.dragonFactory = dragonFactory;
            this.settingsFactory = dragonSettingsFactory;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            dragonFactory.CreateDragonPainter(dragonSettings).Paint();
            //var container = new StandardKernel();
            //container.Bind<IImageHolder>().ToConstant(imageHolder);
            //container.Bind<DragonSettings>().ToConstant(dragonSettings);
            //container.Get<DragonPainter>().Paint();
        }

        private DragonSettings CreateRandomSettings()
        {
            return this.settingsFactory(new Random()).Generate();
        }
    }
}