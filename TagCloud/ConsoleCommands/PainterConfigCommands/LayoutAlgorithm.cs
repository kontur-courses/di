using System;
using System.Collections.Generic;
using System.Linq;
using TagCloudPainter;
using TagsCloudVisualization.Spirals;
using UIConsole;

namespace TagCloud.ConsoleCommands
{
    public class LayoutAlgorithm : IConsoleCommand
    {
        private ISpiral[] spirals;
        private PainterConfig painterConfig;
         
        public LayoutAlgorithm(PainterConfig painterConfig, ISpiral[] spirals)
        {
            this.spirals = spirals;
            this.painterConfig = painterConfig;
        }
        
        public string Name => "LayoutAlgorithm";
        public string Description => "Устанавливает алгоритм раскладки слов";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            var layoutAlgorithmName = args["AlgorithmName"].ToString();
            var layoutAlgorithm = spirals
                .FirstOrDefault(spiral => spiral.Name.Equals(layoutAlgorithmName));

            if (layoutAlgorithm == null)
            {
                throw new Exception($"No algorithm found with the name {layoutAlgorithmName}");
            }

            painterConfig.layoutAlgorithm = layoutAlgorithm;
        }

        public List<string> ArgsName => new List<string>() { "AlgorithmName" };
    }
}