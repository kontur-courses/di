using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloud.ConsoleCommands;
using TagsCloud.Distributors;
using TagsCloud.Layouters;
using TagsCloud.WordSizeCalculators;

namespace TagsCloudTests.Layouters;

public class CircularLayouterTests
{
     [TestFixture]
    public class CircularCloudLayouterTests
    {
        [SetUp]
        public void SetUp()
        {
            center = new Point();
            distributor = new SpiralDistributor(center);
            var options = new Options { TagsFont = "Arial" };
            fontCalculator = new SimpleWordFontCalculator(options);
            tagsCloud = new CircularCloudLayouter(distributor,fontCalculator,center);
        }
        
        private Point center;
        private CircularCloudLayouter tagsCloud;
        private IWordFontCalculator fontCalculator;
        private SpiralDistributor distributor;

        [Test]
        public void CircularCloudLayouter_Initialize_Params()
        {
            tagsCloud.GetTagsCollection().Count().Should().Be(0);
            tagsCloud.Center.Should().Be(center);
        }
        

        [Test]
        public void PutNextRectangle_Should_Place_First_On_Center()
        {
            tagsCloud.CreateTagCloud(new Dictionary<string, int>(){{"Реваванат",3}});
            tagsCloud.GetTagsCollection().ToArray()[0].TagRectangle.Location.Should().Be(center);
        }

        [Test]
        public void CircularCloudLayouter_Should_Has_No_Intersections_When_1000_Words()
        {
            tagsCloud.CreateTagCloud(GetRandomWordsDictionary());
            tagsCloud.GetTagsCollection().Any(tag1 =>
                    tagsCloud.GetTagsCollection().Any(tag2 => tag1.TagRectangle.IntersectsWith(tag2.TagRectangle) && tag1 != tag2))
                .Should().BeFalse();
        }
        
        [Test]
        public void CircularCloudLayouter_Should_Be_Close_To_Circle()
        {
            var randomDict = GetRandomWordsDictionary();
            tagsCloud.CreateTagCloud(randomDict);
            tagsCloud.GetTagsCollection().All(tag =>
            {
                var distanceToCenter =
                    Math.Sqrt(Math.Pow(tag.TagRectangle.X - tagsCloud.Center.X, 2) + Math.Pow(tag.TagRectangle.Y - tagsCloud.Center.Y, 2));
                return distanceToCenter <= distributor.Radius;
            }).Should().BeTrue();
        }
        
        
        private Dictionary<string,int> GetRandomWordsDictionary()
        {
            var random = new Random();
            var dict = new Dictionary<string, int>();
            for (var i = 0; i < 1000; i++)
            {
                dict[$"{i}"] = random.Next(1, 100);
            }
            return dict;
        }
    }
}