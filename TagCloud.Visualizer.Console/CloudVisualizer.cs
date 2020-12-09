using Autofac;
using CommandLine;

namespace TagCloud.Visualizer.Console
{
    internal static class CloudVisualizer
    {
        private static readonly InputOptions InputOptions = new InputOptions();
        private static readonly ImageOptions ImageOptions = new ImageOptions();

        private static readonly CenterOptions CenterOptions =
            new CenterOptions(ImageOptions.ImageWidth / 2, ImageOptions.ImageHeight / 2);

        private static void Main(string[] args)
        {
            var cloudLayouterService = new ContainerBuilder();
            cloudLayouterService.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameter(
                new TypedParameter(typeof(CenterOptions), CenterOptions));
            var cloudLayouterBuilder = cloudLayouterService.Build();
            do
            {
                args = System.Console.ReadLine()?.Split(' ');
                var exitCode = ExecuteCommandAndRerurnExitCode(args, cloudLayouterBuilder);
                if (exitCode == 1)
                {
                    System.Console.WriteLine("Некорректная команда");
                }
            } while (args?[0] != "exit");
        }

        private static int ExecuteCommandAndRerurnExitCode(string[] args, IContainer cloudLayouterBuilder)
        {
            var exitCode = Parser.Default.ParseArguments<PrintCommand, ImageOptions>(args)
                .MapResult(
                    (PrintCommand command) =>
                        command.PrintCloudAndReturnExitCode(cloudLayouterBuilder.Resolve<ICloudLayouter>(),
                            TextReader.GetWords(InputOptions),
                            ImageOptions),
                    (InputOptions opts) => InputOptions.ChangeInputOptionsAndReturnExitCode(opts),
                    (ImageOptions opts) =>
                    {
                        var localExitCode = ImageOptions.ChangeImageOptionsAndReturnExitCode(opts);
                        CenterOptions.X = ImageOptions.ImageWidth / 2;
                        CenterOptions.Y = ImageOptions.ImageHeight / 2;
                        return localExitCode;
                    },
                    errs => 1);
            return exitCode;
        }
    }
}