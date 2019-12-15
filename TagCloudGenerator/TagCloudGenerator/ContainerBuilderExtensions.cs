using Autofac;

namespace TagCloudGenerator
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterDecorator<TInterface, TImplementation, TDecorator>(this ContainerBuilder builder)
            where TImplementation : TInterface
            where TDecorator : TInterface
        {
            var innerImplementationName = $"{typeof(TImplementation).Name} as implementation";
            var decoratorName = $"{typeof(TDecorator).Name} as decorator";

            builder.RegisterType<TImplementation>().Named<TInterface>(innerImplementationName).PropertiesAutowired();
            builder.RegisterType<TDecorator>().Named<TInterface>(decoratorName);
            builder.RegisterDecorator<TInterface>(
                (c, inner) => c.ResolveNamed<TInterface>(decoratorName, TypedParameter.From(inner)),
                innerImplementationName);
        }
    }
}