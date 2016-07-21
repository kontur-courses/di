using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App
{
	internal static class Program
	{
		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			var container = new StandardKernel();
			container.Bind<IUiAction>().To<SaveImageAction>();
			container.Bind<IUiAction>().To<KochFractalAction>();
			container.Bind<IUiAction>().To<DragonFractalAction>();
			container.Bind<IUiAction>().To<ImageSettingsAction>();
			container.Bind<IUiAction>().To<PaletteSettingsAction>();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(container.Get<MainForm>());
		}
	}
}