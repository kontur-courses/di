using System;
using FractalPainting.Infrastructure;
using FractalPainting.Solved.App.Fractals;

namespace FractalPainting.Solved.App.Actions
{
	public class DragonFractalAction : IUiAction
    {
        private readonly Func<DragonSettings, DragonPainter> createDragonPainer;

        public DragonFractalAction(Func<DragonSettings, DragonPainter> createDragonPainer)
        {
            this.createDragonPainer = createDragonPainer;
        }

		public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";

		public void Perform()
		{
            var dragonSettings = new DragonSettingsGenerator(new Random()).Generate();
            SettingsForm.For(dragonSettings).ShowDialog();
            createDragonPainer(dragonSettings).Paint();
		}
	}
}