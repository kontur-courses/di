using System;
using System.IO;
using Autofac;

namespace TagsCloud
{
  internal static class Program
  {
    public static void Main(string[] args)
    {
        var container = ContainerConstructor.Configure(args);
        var app = container.Resolve<Application>();
        app.Run();
    }
  }
}