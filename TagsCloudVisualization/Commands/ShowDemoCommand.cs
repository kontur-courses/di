using CommandLine;

namespace TagsCloudVisualization.Commands
{
    [Verb("show-demo", HelpText = "Генерирует тестовые изображения в качестве демонстрации.")]
    public class ShowDemoCommand
    {
        [Option('o', "outputFolder", Required = true,
            HelpText = "Путь, куда будет сохранен результат.")]
        public string OutputPath { get; set; }
    }
}