using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using WeCantSpell.Hunspell;

namespace TagCloudGenerator
{
    public class TextProcessor : ITextProcessor
    {
        public string ProcessTheText(string filePath) 
        {
            var text = File.ReadAllText(filePath);
            // var result = new StringBuilder();

            //foreach (var line in text)
            //{


            //    result.Append(line.ToLower());
            //}
            
            return text.ToLower();
        }
    }
}
