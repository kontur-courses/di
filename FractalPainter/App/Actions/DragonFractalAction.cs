using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction//, INeed<IImageHolder>
    {
        //private IImageHolder imageHolder;
        /*
        public void SetDependency(IImageHolder dependency)
        {
            imageHolder = dependency;
        }
        */
        private IDragonFactoryFabric dragonPainter;
        private readonly Func<Random, DragonSettingsGenerator> createDragonSettingsGenerator;

        public DragonFractalAction(IDragonFactoryFabric _dragonPainter,
            Func<Random, DragonSettingsGenerator> createDragonSettingsGenerator)
        {
            dragonPainter = _dragonPainter;
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
            dragonPainter.Create(dragonSettings).Paint();
        }

        private DragonSettings CreateRandomSettings()
        {
            return createDragonSettingsGenerator(new Random()).Generate();
        }        
    }
}