using Autofac;
using Mono.Options;
using System.Reflection;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer;
public class Program
{
    public static void Main(string[] args)
    {
        InitializationHelper.RunWithArgs(args);
    }

    

}
