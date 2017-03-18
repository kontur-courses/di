using System;
using FractalPainting.Infrastructure;
using FractalPainting.Solved.App.Fractals;

namespace FractalPainting.Solved.App.Actions
{
	public class DragonFractalAction : IUiAction
    {
        private readonly Func<DragonSettings, DragonPainter> createDragonPainer;
        private readonly IDragonSettingsGenerator dragonSettingsGenerator;

        public DragonFractalAction(Func<DragonSettings, DragonPainter> createDragonPainer, IDragonSettingsGenerator dragonSettingsGenerator)
        {
            this.createDragonPainer = createDragonPainer;
            this.dragonSettingsGenerator = dragonSettingsGenerator;
        }

		public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";

		public void Perform()
		{
            var dragonSettings = dragonSettingsGenerator.Generate();
            SettingsForm.For(dragonSettings).ShowDialog();
            createDragonPainer(dragonSettings).Paint();
		}
	}
}