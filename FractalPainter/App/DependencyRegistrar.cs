using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace FractalPainting.App
{
    public class DependencyRegistrar : NinjectModule
    {
        public override void Load()
        {
            // Bind<IUiAction>().To<SaveImageAction>();
            // Bind<IUiAction>().To<DragonFractalAction>();
            // Bind<IUiAction>().To<KochFractalAction>();
            // Bind<IUiAction>().To<ImageSettingsAction>();
            // Bind<IUiAction>().To<PaletteSettingsAction>();


            Kernel.Bind(convention =>
                convention.FromThisAssembly()
                    .SelectAllClasses()
                    .InheritedFrom<IUiAction>()
                    .BindAllInterfaces());

            Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
            Bind<Palette>().ToSelf().InSingletonScope();

            Bind<IDragonPainterFactory>().ToFactory();

            Bind<IObjectSerializer>().To<XmlObjectSerializer>();
            Bind<IBlobStorage>().To<FileBlobStorage>();

            Bind<IImageDirectoryProvider, AppSettings>()
                .ToMethod(context => context.Kernel.Get<SettingsManager>().Load()).InSingletonScope();

            Bind<ImageSettings>().ToMethod(context => context.Kernel.Get<AppSettings>().ImageSettings)
                .InSingletonScope();
        }
    }
}