using Autofac;

namespace TagsCloud.Console
{
    public interface IConsoleUI
    {
        public void Run(IAppSettings settings);
    }
}