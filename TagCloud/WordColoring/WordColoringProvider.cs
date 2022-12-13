using System;
using System.Linq;

namespace TagCloud.WordColoring
{
    public class WordColoringProvider : IWordColoringProvider
    {
        private readonly IWordColoring[] colorings;

        public WordColoringProvider()
        {
            colorings = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                        .Where(t => t.GetInterfaces().Contains(typeof(IWordColoring)))
                        .Select(t => (IWordColoring)Activator.CreateInstance(t))
                        .ToArray();
        }

        public IWordColoring GetWordColoringByName(string name)
        {
            var coloring = colorings.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));

            if (coloring is null)
                throw new ArgumentException($"Word coloring algorithm <{name}> doesn't exist");

            return coloring;
        }
    }
}
