using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class Capitalizer : IWordNormalizer
{
    public string Normalize(string word)
    {
        return $"{char.ToUpper(word[0])}{word[1..]}";
    }
}
