using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public interface IDragonPainterFactory
    {
        DragonPainter Create(DragonSettings dragonSettings);
    }
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory factory;
        private readonly Func<Random, DragonSettingsGenerator> createDragonSettingsGenerator;

        public DragonFractalAction(IDragonPainterFactory factory, 
            Func<Random,DragonSettingsGenerator> createDragonSettingsGenerator)
        {
            this.factory = factory;
            this.createDragonSettingsGenerator = createDragonSettingsGenerator;
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
            //var container = new StandardKernel();
            //container.Bind<IImageHolder>().ToConstant(imageHolder);
            //container.Bind<DragonSettings>().ToConstant(dragonSettings);
            //container.Get<DragonPainter>().Paint();
            factory.Create(dragonSettings).Paint();
        }

        private DragonSettings CreateRandomSettings()
        {
            return createDragonSettingsGenerator(new Random()).Generate();
        }
    }
}