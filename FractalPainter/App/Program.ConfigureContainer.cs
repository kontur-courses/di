using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static partial class Program
    {
        private static void ConfigureContainer(IKernel container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("Container can't be null.");
            }
            container.Bind<IUiAction>().To<SaveImageAction>();
            container.Bind<IUiAction>().To<DragonFractalAction>();
            container.Bind<IUiAction>().To<KochFractalAction>();
            container.Bind<IUiAction>().To<ImageSettingsAction>();
            container.Bind<IUiAction>().To<PaletteSettingsAction>();

            container.Bind<Palette>().ToSelf()
                .InSingletonScope();
            container.Bind<IImageHolder, PictureBoxImageHolder>()
                .To<PictureBoxImageHolder>()
                .InSingletonScope();

            container.Bind<IObjectSerializer>().To<XmlObjectSerializer>()
                    .WhenInjectedInto<SettingsManager>();
            container.Bind<IBlobStorage>().To<FileBlobStorage>()
                .WhenInjectedInto<SettingsManager>();
            container.Bind<AppSettings, IImageDirectoryProvider>()
                .ToMethod(context => context.Kernel.Get<SettingsManager>().Load())
                .InSingletonScope();
            //TODO:Now the default settings from the file!!!
            container.Bind<ImageSettings>()
                .ToMethod(context => context.Kernel.Get<SettingsManager>().Load().ImageSettings)
                .InSingletonScope();
            // container.Bind<IDragonPainterFactory>().ToFactory();//TODO:5.1

        }
    }
}

