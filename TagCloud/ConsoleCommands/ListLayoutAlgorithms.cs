using System;
using System.Collections.Generic;
using System.Text;
using TagsCloudVisualization.Spirals;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class ListLayoutAlgorithms : IConsoleCommand
    {
        private ISpiral[] spirals;
        
        public ListLayoutAlgorithms(ISpiral[] spirals)
        {
            this.spirals = spirals;
        }
        
        public string Name => "LayoutAlgorithms";
        public string Description => "Выводить список возможных алгоритмов";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            var output = new StringBuilder();
            output.Append("Layout algorithms:" + Environment.NewLine);
            foreach (var spiral in spirals)
            {
                output.Append($"\t{spiral.Name}" + Environment.NewLine);
            }
            
            console.PrintInConsole(output.ToString());
        }

        public List<string> ArgsName => new List<string>();
    }
}