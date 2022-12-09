using Autofac;

namespace App;

public interface IApplication
{
    public void Run(IContainer container, IEnumerable<string> args);
}