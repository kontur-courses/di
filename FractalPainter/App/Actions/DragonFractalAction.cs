using System;
using FractalPainting.Fractals;
using FractalPainting.Infrastructure;

namespace FractalPainting.App.Actions
{
	public class DragonFractalAction : IUiAction
	{
		private readonly Func<DragonSettings, DragonPainter> createPainter;

		public DragonFractalAction(Func<DragonSettings, DragonPainter> createPainter)
		{
			this.createPainter = createPainter;
		}

		public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";

		public void Perform()
		{
			var dragonSettings = new DragonSettingsGenerator(new Random()).Generate();
			SettingsForm.For(dragonSettings).ShowDialog();
			createPainter(dragonSettings).Paint();
		}
	}
}