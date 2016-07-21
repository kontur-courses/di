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
			container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
			container.Bind<IObjectSerializer>().To<XmlObjectSerializer>().WhenInjectedInto<SettingsManager>();
			container.Bind<IBlobStorage>().To<FileBlobStorage>().WhenInjectedInto<SettingsManager>();
			container.Bind<IImageDirectoryProvider, AppSettings>()
				.ToMethod(context => context.Kernel.Get<SettingsManager>().Load()).InSingletonScope();
			container.Bind<Palette>().ToSelf().InSingletonScope();
			container.Bind<ImageSettings>()
				.ToMethod(context => context.Kernel.Get<AppSettings>().ImageSettings);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				Application.Run(container.Get<MainForm>());
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}
	}
}