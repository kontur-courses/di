using System.Collections.Generic;
using CommandLine;

namespace TagsCloudVisualization.Commands
{
    [Verb("create-cloud", HelpText = "Генерирует облако тегов по текстовому файлу.")]
    public class CreateCloudCommand : CommandLineOptions
    {
    }
}