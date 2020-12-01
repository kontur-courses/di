using System;
using System.Drawing;
using TagsCloudVisualization;

namespace CloudContainer.ConfigCreators
{
    public class ConsoleConfigCreator : IConfigCreator
    {
        public IWordConfig CreateConfig(string[] args)
        {
            if(args.Length != 6)
                throw new ArgumentException();
            var centerText = "";
            var fontText = "";
            var colorText = "";
            
            for (var i = 0; i < args.Length; i += 2)
            {
                if (args[i] == "-ce")
                    centerText = args[i + 1];
                if (args[i] == "-f")
                    fontText = args[i + 1];
                if (args[i] == "-c")
                    colorText = args[i + 1];
            }
            if(centerText == "" || fontText == "" || colorText == "")
                throw new ArgumentException();
            var splittedCenter = centerText.Split(';');
            var center = new Point(Convert.ToInt32(splittedCenter[0]), Convert.ToInt32(splittedCenter[1]));
            var color = Color.FromName(colorText);
            var splittedFont = fontText.Split(';');
            var font = new Font(splittedFont[0], Convert.ToInt32(splittedFont[1]));
            return new WordConfig(font,center,color);
        }
    }
}