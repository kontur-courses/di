using System;
using FractalPainting.Fractals;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App.Actions
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
			var dragonSettings = new DragonSettingsGenerator(new Random()).Generate();
			SettingsForm.For(dragonSettings).ShowDialog();
			var container = new StandardKernel();
			container.Bind<IImageHolder>().ToConstant(imageHolder);
			container.Bind<DragonSettings>().ToConstant(dragonSettings);
			container.Get<DragonPainter>().Paint();
		}
	}
}