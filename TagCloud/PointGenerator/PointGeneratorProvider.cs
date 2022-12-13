using System;
using System.Drawing;
using System.Linq;

namespace TagCloud.PointGenerator
{
    public class PointGeneratorProvider
    {
        private readonly IPointGenerator[] generators;

        public PointGeneratorProvider()
        {
            generators = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                            .Where(t => t.GetInterfaces().Contains(typeof(IPointGenerator)))
                            .Select(t => (IPointGenerator)Activator.CreateInstance(t))
                            .ToArray();
        }

        public IPointGenerator GetPointGenerator(string form)
        {
            var generator = generators.FirstOrDefault(g => g.ToString().ToLower().Contains(form.ToLower()));

            if (generator is null)
                throw new ArgumentException($"Tag cloud form <{form}> isn't available");

            return generator;
        }
    }
}
