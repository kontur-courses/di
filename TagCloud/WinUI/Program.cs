using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;
using TagsCloudVisualisation;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Output;
using TagsCloudVisualisation.Text;
using TagsCloudVisualisation.Visualisation;

namespace WinUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = InitContainer();
            container.Resolve<App>().Subscribe();

            var form = container.Resolve<MainForm>();
            Application.Run(form);
        }

        private static IContainer InitContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FileWordsReader>()
                .AsImplementedInterfaces()
                .AsSelf()
                .WithParameter("delimiters", new[] {',', '.', ' ', '!', '?', '\n', '\r', '\t', '&', '#', '-'});

            builder.RegisterType<CircularTagCloudLayouter>()
                .AsImplementedInterfaces()
                .AsSelf()
                .WithParameter("cloudCenter", new Point())
                .WithParameter("minRectangleSize", new Size(3, 3));

            builder.RegisterType<FileResultWriter>()
                .AsImplementedInterfaces()
                .AsSelf()
                .WithParameter("format", ImageFormat.Png);

            builder.RegisterType<CloudVisualiser>()
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<TagCloudGenerator>()
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterGeneric((c, gt) =>
                {
                    var baseType = gt.Single();
                    var factories = c.ComponentRegistry.Registrations
                        .Where(x => baseType.IsAssignableFrom(x.Activator.LimitType))
                        .Select(t => typeof(DefaultParametrizedFactory<>).MakeGenericType(t.Activator.LimitType))
                        .Select(Activator.CreateInstance)
                        .ToArray();

                    var resultType = typeof(DependenciesFactoryCollection<>).MakeGenericType(baseType);
                    return Activator.CreateInstance(resultType, factories);
                })
                .As(typeof(IFactoryCollection<>));

            builder.RegisterAssemblyTypes(typeof(ITagCloudLayouter).Assembly, typeof(Program).Assembly)
                .Where(t => !builder.ComponentRegistryBuilder.IsRegistered(new TypedService(t)))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();

            return builder.Build();
        }

        // TODO всё что ниже - эксперименты, заюзать или удалить
        public interface IParametrizedFactory<out T> where T : class
        {
            IEnumerable<CtorParameter> RequiredParameters { get; }
            T Create(IDictionary<string, object> parameters);
        }

        public class DefaultParametrizedFactory<T> : IParametrizedFactory<T> where T : class
        {
            private readonly CtorParameter[] requiredParameters;
            private readonly ConstructorInfo ctor;

            public DefaultParametrizedFactory()
            {
                var type = typeof(T);
                ctor = type.GetConstructors().Single();
                requiredParameters = ctor.GetParameters()
                    .Select(x => new CtorParameter(x.ParameterType, x.Name!))
                    .ToArray();
            }

            public IEnumerable<CtorParameter> RequiredParameters => requiredParameters;

            public T Create(IDictionary<string, object> parameters)
            {
                if (parameters.Count != requiredParameters.Length)
                    throw new ArgumentException("Wrong parameters count: " +
                                                $"required {requiredParameters.Length}, " +
                                                $"but passed {parameters.Count}");

                var ctorParameters = requiredParameters.Select(r => GetParameterOrThrow(r, parameters)).ToArray();
                return (T) ctor.Invoke(ctorParameters);
            }

            private static object GetParameterOrThrow(CtorParameter required, IDictionary<string, object> parameters)
            {
                if (!parameters.TryGetValue(required.Name, out var passed))
                    throw new ArgumentException("Parameters collection doesnt contain " +
                                                $"{required.Name} [{required.Type}]");

                if (!required.Type.IsInstanceOfType(passed))
                    throw new ArgumentException($"Parameter {required.Name} has wrong type: " +
                                                $"expected [{required.Type}], " +
                                                $"but was [{passed?.GetType().FullName ?? "null"}]");

                return passed;
            }
        }

        public class CtorParameter
        {
            public CtorParameter(Type type, string name)
            {
                Type = type;
                Name = name;
            }

            public Type Type { get; }
            public string Name { get; }

            public override string ToString() => $"{Name} [{Type}]";
        }

        public interface IFactoryCollection<out T> : IEnumerable<IParametrizedFactory<T>> where T : class
        {
            IEnumerable<IParametrizedFactory<T>> Registered { get; }
        }

        public class DependenciesFactoryCollection<T> : IFactoryCollection<T> where T : class
        {
            public DependenciesFactoryCollection(params IParametrizedFactory<T>[] registered)
            {
                Registered = registered;
            }

            public IEnumerable<IParametrizedFactory<T>> Registered { get; }

            public IEnumerator<IParametrizedFactory<T>> GetEnumerator() => Registered.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}