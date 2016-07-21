using System;
using FractalPainting.Fractals;
using FractalPainting.Infrastructure;
using Ninject;
using Ninject.Syntax;

namespace FractalPainting.App.Actions
{
	public class DragonFractalAction : IUiAction, INeed<IImageHolder>
	{
		private IImageHolder imageHolder;
		public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";

		public void Perform()
		{
			var dragonSettings = new DragonSettings();
			SettingsForm.For(dragonSettings).ShowDialog();

			var container = new StandardKernel();
			container.Bind<IImageHolder>().ToConstant(imageHolder);
			container.Bind<DragonSettings>().ToConstant(dragonSettings);
			container.Get<DragonPainter>().Paint();
		}

		public void SetDependency(IImageHolder dependency)
		{
			imageHolder = dependency;
		}
	}

	//container.Bind<ImageSettings>().ToPart().Of<AppSettings>(s => s.ImageSettings);
	public static class DependencyProviderKernelExtensions
	{
		public static PartSyntax<TDep> ToPart<TDep>(
			this IBindingToSyntax<TDep> syntax)
		{
			return new PartSyntax<TDep>(syntax);
		}
	}

	public class PartSyntax<T>
	{
		private readonly IBindingToSyntax<T> syntax;

		public PartSyntax(IBindingToSyntax<T> syntax)
		{
			this.syntax = syntax;
		}
		public IBindingWhenInNamedWithOrOnSyntax<T> Of<TPartHolder>(Func<TPartHolder, T> getDependency)
		{
			return syntax.ToMethod(ctx => getDependency(ctx.Kernel.Get<TPartHolder>()));
		}
	}
}