using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using TagsCloudVisualization.Default;

namespace TagsCloudVisualization
{
    public class TagCloudFactory
    {
        private static readonly Dictionary<string, ITokenOrderer> Orders = new Dictionary<string, ITokenOrderer>()
        {
            ["sorted"] = new TokenSortedOrder(),
            ["mixed"] = new TokenShuffler()
        };

        public TagCloud CreateInstance(bool manhattan, string order)
        {
            var metric = manhattan ? 
                (Func<PointF, PointF, float>)CircularCloudMaker.ManhattanDistance :
                CircularCloudMaker.Distance;
            if (order == null || !Orders.ContainsKey(order))
                throw new ArgumentException("Unknown order");
            var orderer = Orders[order];
            var builder = new ContainerBuilder();
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.RegisterType<RandomTagColor>().As<ITokenColorChooser>();
            builder.RegisterType<WordCounter>().As<ITokenWeigher>();
            builder.RegisterType<WordSelector>().UsingConstructor().As<IWordSelector>();
            builder.RegisterType<FileReader>();
            builder.RegisterType<TokenGenerator>();
            builder.RegisterType<TagCloudMaker>();
            builder.RegisterType<TagCloudVisualiser>();
            builder.RegisterType<TagCloud>();
            builder.RegisterInstance(orderer).As<ITokenOrderer>();
            builder.RegisterInstance(new CircularCloudMaker(Point.Empty, metric)).As<ICloudMaker>();
            var container = builder.Build();
            using var scope = container.BeginLifetimeScope();
            return scope.Resolve<TagCloud>();
        }
    }
}