using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Autofac;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    [Ignore("Visual test")]
    public class TagsCloudVisualization_Should
    {
        private ContainerProvider provider;

        [OneTimeSetUp]
        public void SetUpOnce()
        {
            provider = new ContainerProvider();
            provider.RegisterDependencies();
        }

        [Test]
        public void ChangeBackgroundColor()
        {
            var config = new ScreenConfig {BackgroundColor = Color.Aqua, Size = new Size(800, 600)};
            
            using var scope = provider.Container.BeginLifetimeScope(b => 
                b.Register(_ => config).As<ScreenConfig>().SingleInstance());
            
            var creator = scope.Resolve<ITagCloudCreator>();
            var img = creator.CreateFromFile(@"..\..\..\words.txt");
            img.Save(@"..\..\..\TestVisualizations\ChangedBgColor.png");
        }

        [Test]
        public void AddBoringWord()
        {
            var boringWords =
                provider.Container.Resolve<IEnumerable<Func<string, bool>>>()
                    .Append(s => s == "known");
            
            using var scope = provider.Container.BeginLifetimeScope(b => 
                b.Register(_ => boringWords).As<IEnumerable<Func<string, bool>>>().SingleInstance());
            
            var creator = scope.Resolve<ITagCloudCreator>();
            var img = creator.CreateFromFile(@"..\..\..\words.txt");
            img.Save(@"..\..\..\TestVisualizations\AddedBoringWords.png");
        }
        
        [Test]
        public void ChangeWordsColor()
        {
            Func<string, Color> colorizer = s => Color.Crimson;
            
            using var scope = provider.Container.BeginLifetimeScope(b => 
                b.Register(_ => colorizer).As<Func<string, Color>>().SingleInstance());
            
            var creator = scope.Resolve<ITagCloudCreator>();
            var img = creator.CreateFromFile(@"..\..\..\words.txt");
            img.Save(@"..\..\..\TestVisualizations\ChangedWordsColor.png");
        }
        
        [Test]
        public void ChangeFontSize()
        {
            var fontSize = 20;
            
            using var scope = provider.Container.BeginLifetimeScope(b => 
                b.Register(_ => fontSize).As<int>().SingleInstance());
            
            var creator = scope.Resolve<ITagCloudCreator>();
            var img = creator.CreateFromFile(@"..\..\..\words.txt");
            img.Save(@"..\..\..\TestVisualizations\ChangedFontSize.png");
        }
        
        [Test]
        public void ChangePreparations()
        {
            string Preparation(string str)
            {
                var newStr = new StringBuilder();

                for (var i = 0; i < str.Length; ++i)
                {
                    newStr.Append(i % 2 == 0 ? char.ToUpper(str[i]) : str[i]);
                }

                return newStr.ToString();
            }
            
            var preparations = provider.Container.Resolve<IEnumerable<Func<string, string>>>()
                .Append(Preparation);
            
            using var scope = provider.Container.BeginLifetimeScope(b => 
                b.Register(_ => preparations).As<IEnumerable<Func<string, string>>>().SingleInstance());
            
            var creator = scope.Resolve<ITagCloudCreator>();
            var img = creator.CreateFromFile(@"..\..\..\words.txt");
            img.Save(@"..\..\..\TestVisualizations\ChangedPreparations.png");
        }

        [Test]
        public void ChangeImageSizeAndCenter()
        {
            var config = new ScreenConfig {BackgroundColor = Color.White, Size = new Size(1920, 1080)};
            var center = new Point(960, 540);
            
            using var scope = provider.Container.BeginLifetimeScope(b =>
            {
                b.Register(_ => config).As<ScreenConfig>().SingleInstance();
                b.Register(_ => center).As<Point>().SingleInstance();
            });
            
            var creator = scope.Resolve<ITagCloudCreator>();
            var img = creator.CreateFromFile(@"..\..\..\words.txt");
            img.Save(@"..\..\..\TestVisualizations\ChangedImageSizeAndCenter.png");
        }

        [Test]
        public void MultipleChanges()
        {
            string Preparation(string str)
            {
                var newStr = new StringBuilder();

                for (var i = 0; i < str.Length; ++i)
                {
                    newStr.Append(i % 2 == 0 ? char.ToUpper(str[i]) : str[i]);
                }

                return newStr.ToString();
            }
            
            var preparations = provider.Container.Resolve<IEnumerable<Func<string, string>>>()
                .Append(Preparation);
            Func<string, Color> colorizer = s => Color.Crimson;
            var config = new ScreenConfig {BackgroundColor = Color.Aqua, Size = new Size(1920, 1080)};
            var center = new Point(960, 540);
            var fontSize = 32;

            using var scope = provider.Container.BeginLifetimeScope(b =>
            {
                b.Register(_ => config).As<ScreenConfig>().SingleInstance();
                b.Register(_ => center).As<Point>().SingleInstance();
                b.Register(_ => preparations).As<IEnumerable<Func<string, string>>>().SingleInstance();
                b.Register(_ => fontSize).As<int>().SingleInstance();
                b.Register(_ => colorizer).As<Func<string, Color>>().SingleInstance();
            });
            
            var creator = scope.Resolve<ITagCloudCreator>();
            var img = creator.CreateFromFile(@"..\..\..\words.txt");
            img.Save(@"..\..\..\TestVisualizations\MultipleChanges.png");
        }
    }
}