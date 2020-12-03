using Autofac;

namespace WordCloudGenerator
{
    public interface IUserInterface
    {
        public void Run(IContainer container);
    }
}