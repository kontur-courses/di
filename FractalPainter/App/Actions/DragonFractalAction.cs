using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly IDragonPainterFactory painterFactory;
        private readonly Func<Random, DragonSettingsGenerator> dragonSettingsGenerator;

        public DragonFractalAction(IImageHolder imageHolder, IDragonPainterFactory dragonPainterFactory,
            Func<Random, DragonSettingsGenerator> dragonSettingsGenerator)
        {
            this.imageHolder = imageHolder;
            this.dragonSettingsGenerator = dragonSettingsGenerator;
            painterFactory = dragonPainterFactory;
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
            painterFactory.CreateDragonPainter(imageHolder, dragonSettings).Paint();
        }

        private DragonSettings CreateRandomSettings()
        {
            return dragonSettingsGenerator(new Random()).Generate();
        }
    }
}