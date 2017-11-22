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
		private readonly IDragonPainterFactory dragonPainterFactory;
		private readonly Func<Random, DragonSettingsGenerator> dragonSettingFactory;

		public DragonFractalAction(IDragonPainterFactory dragonPainterFactory, 
			Func<Random, DragonSettingsGenerator> dragonSettingFactory)
		{
			this.dragonPainterFactory = dragonPainterFactory;
			this.dragonSettingFactory = dragonSettingFactory;
		}


		public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";

		public void Perform()
		{
			var dragonSettings = dragonSettingFactory(new Random()).Generate();
			// редактируем настройки:
			SettingsForm.For(dragonSettings).ShowDialog();
			
			dragonPainterFactory.CreateDragonPainter(dragonSettings).Paint();
		}


	}
}