using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Autofac;
using NUnit.Framework;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Layout;
using TagCloud.Infrastructure.Layout.Environment;
using TagCloud.Infrastructure.Layout.Strategies;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloudTests
{
    [TestFixture]
    public class ColorPickerTests
    {
        private ColorPicker colorPicker;
        private IContainer container;

        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Settings>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<PlainEnvironment>().AsImplementedInterfaces();
            builder.RegisterType<SpiralStrategy>().As<ILayoutStrategy>();
            builder.RegisterType<TagCloudLayouter>().As<ILayouter<Size, Rectangle>>();
            
            builder.RegisterType<Random>().SingleInstance();
            builder.RegisterType<ColorPicker>();

            container = builder.Build();
            
            colorPicker = container.Resolve<ColorPicker>();
        }

        [Test]
        public void GetColor_PickRandomColors_ForDifferentWordTypes()
        {
            var picked = new HashSet<Color>();
            var infos = new List<TokenInfo>();
            for (var type = WordType.UNKNOWN; type < WordType.V; type++)
                infos.Add(new TokenInfo(type));

            foreach (var color in infos.Select(info => colorPicker.GetColor(info)))
            {
                var isAdded = picked.Add(color);
                Assert.True(isAdded, $"already contains color {color}");
            }
        }
        
        [Test]
        public void GetColor_SameColor_ForWordOfSameType()
        {
            var typesFirstRun = new Dictionary<WordType, Color>();
            var typesSecondRun = new Dictionary<WordType, Color>();
            
            for (var type = WordType.UNKNOWN; type < WordType.V; type++)
            {
                var tokenInfo = new TokenInfo(type);
                typesFirstRun.Add(tokenInfo.WordType, colorPicker.GetColor(tokenInfo));
            }
            for (var type = WordType.UNKNOWN; type < WordType.V; type++)
            {
                var tokenInfo = new TokenInfo(type);
                typesSecondRun.Add(tokenInfo.WordType, colorPicker.GetColor(tokenInfo));
            }

            CollectionAssert.AreEqual(typesFirstRun, typesSecondRun);
        }
    }
}