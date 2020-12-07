using Autofac;
using NUnit.Framework;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Settings.UISettingsManagers;
using TagCloud.Infrastructure.Text;

namespace TagCloudTests
{
    [TestFixture]
    public class ImageSizeSettingsManagerTests
    {
        private IContainer container;
        private ImageSizeSettingsManager sizeSettingManager;

        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TxtReader>().As<IReader<string>>();

            builder.RegisterType<Settings>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ImageSizeSettingsManager>().AsImplementedInterfaces();
            container = builder.Build();
            sizeSettingManager = container.Resolve<ISettingsManager>() as ImageSizeSettingsManager;
        }

        [Test]
        [TestCase("0 0", true)]
        [TestCase("1 1", true)]
        [TestCase("01 1", true, TestName = "fix zero prefix width")]
        [TestCase("1 01", true, TestName = "fix zero prefix height")]
        [TestCase("01 01", true, TestName = "fix zero prefix width weight")]
        [TestCase("-1 -1", false, TestName = "negative")]
        [TestCase("1000 200", true, TestName = "normal")]
        [TestCase("1000    200", true, TestName = "whitespace")]
        [TestCase("1000\t200", true, TestName = "tab")]
        [TestCase("1000\n200", true, TestName = "new line")]
        [TestCase("123,321", false, TestName = "incorrect separator comma")]
        [TestCase("123.321", false, TestName = "incorrect separator dot")]
        [TestCase("123 . 321", false, TestName = "incorrect separator with white spaces")]
        [TestCase("1 23 321", false, TestName = "multiple numbers separated by space")]
        [TestCase("1.23 321", false, TestName = "aka double")]
        public void TrySet_IsCorrect(string input, bool expected)
        {
            var actual = sizeSettingManager.TrySet(input);

            Assert.That(actual, Is.EqualTo(expected));
        }
        
        [Test]
        [TestCase("0 0", "0 0")]
        [TestCase("1 1", "1 1")]
        [TestCase("01 1", "1 1", TestName = "fix zero prefix width")]
        [TestCase("1 01", "1 1", TestName = "fix zero prefix height")]
        [TestCase("01 01","1 1", TestName = "fix zero prefix width weight")]
        [TestCase("1000 200","1000 200", TestName = "normal")]
        [TestCase("1000    200", "1000 200", TestName = "whitespace")]
        [TestCase("1000\t200","1000 200", TestName = "tab")]
        [TestCase("1000\n200", "1000 200", TestName = "new line")]
        public void Get_AfterSet(string input, string expected)
        {
            var isSuccess = sizeSettingManager.TrySet(input);
            var got = sizeSettingManager.Get();
            
            Assert.True(isSuccess);
            Assert.That(got, Is.EqualTo(expected));
        }
        
        
    }
}