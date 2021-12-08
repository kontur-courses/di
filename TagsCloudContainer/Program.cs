using Autofac;
using Mono.Options;
using System.Diagnostics;
using System.Reflection;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults;
using TagsCloudContainer.Defaults.MyStem;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer;
public class Program
{
    public static void Main(string[] args)
    {
        args = new[] { "--string", @"Повседневная практика показывает, что реализация намеченных плановых заданий в значительной степени обуславливает создание модели развития. Повседневная практика показывает, что укрепление и развитие структуры обеспечивает широкому кругу (специалистов) участие в формировании дальнейших направлений развития.
Идейные соображения высшего порядка, а также рамки и место обучения кадров обеспечивает широкому кругу (специалистов) участие в формировании новых предложений. Не следует, однако забывать, что дальнейшее развитие различных форм деятельности способствует подготовки и реализации форм развития.
Товарищи! сложившаяся структура организации представляет собой интересный эксперимент проверки направлений прогрессивного развития. Равным образом рамки и место обучения кадров влечет за собой процесс внедрения и модернизации системы обучения кадров, соответствует насущным потребностям.", "--max-size","1000","--height","1000","--width","1400","--center", "500, 300", "--color", "red" };

        var assemblies = new List<Assembly>() { Assembly.GetExecutingAssembly() };
        var assemblyAdder = new OptionSet()
        {
            {"assemblies=",$"Specifies additional assemblies to use.",v => AddAssembliesFrom(assemblies,v.Split()) }
        };
        var leftAgrs = assemblyAdder.Parse(args);
        var builder = new ContainerBuilder();
        RegistrationHelper.RegisterServices(builder, assemblies.ToArray());
        var container = builder.Build();

        IRunner runner = container.Resolve<DefaultRunner>();
        var runnerSelector = new OptionSet()
        {
            {"runner=",$"Select runner to use. Defaults to {nameof(DefaultRunner)}",v => runner = container.ResolveKeyed<IRunner>(v) }
        };

        leftAgrs = runnerSelector.Parse(leftAgrs);

        runner.Run(leftAgrs.ToArray());
    }

    private static void AddAssembliesFrom(List<Assembly> assemblies, IEnumerable<string> assemblyNames)
    {
        foreach (var assembly in assemblyNames)
        {
            assemblies.Add(Assembly.LoadFrom(assembly));
        }
    }
}
