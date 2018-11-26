using System;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step03.App.Fractals;
using FractalPainting.Solved.Step03.Infrastructure.Injection;
using FractalPainting.Solved.Step03.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.Solved.Step03.App.Actions
{
    public class DragonFractalAction : IUiAction, INeed<IImageHolder>
    {
        private IImageHolder imageHolder;

        public void SetDependency(IImageHolder dependency)
        {
            imageHolder = dependency;
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
            var container = new StandardKernel();
            container.Bind<IImageHolder>().ToConstant(imageHolder);
            container.Bind<DragonSettings>().ToConstant(dragonSettings);
            container.Get<DragonPainter>().Paint();
        }

        private static DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }
    }
}