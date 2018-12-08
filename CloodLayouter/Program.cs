using System.Reflection;
using Autofac;
using CloodLayouter.App;
using CloodLayouter.Infrastructer;

namespace CloodLayouter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var logicBuilder = new ContainerBuilder();

            logicBuilder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces().SingleInstance();
            var logicContainer = logicBuilder.Build();
            
            //Можно было заинжектить в один класс и один раз вызвать Resolve, но я оставил так для наглядности 
            logicContainer.Resolve<IStreamReader>().Read();
            logicContainer.Resolve<IWordSlector>().Select();
            logicContainer.Resolve<IConverter>().Convert();
            logicContainer.Resolve<IDrawer>().Draw();
            logicContainer.Resolve<IImageSaver>().Save();
        }
    }
}