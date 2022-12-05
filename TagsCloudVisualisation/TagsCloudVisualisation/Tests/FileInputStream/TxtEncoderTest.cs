using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualisation.InputStream.FileInputStream;

namespace TagsCloudVisualisation.Tests.FileInputStream
{
    [TestFixture]
    public class TxtEncoderTest
    {
        private string path = "";
        
        [TestCase("text")]
        [TestCase("text1\ntext2")]
        public void GetText_ShouldReturnTextFromTxtFile(string text)
        {
            var sut = new TxtEncoder();
            path = CreateFileWithText(text);
            sut.GetText(path).Should().Be(text);
        }

        [TearDown]
        public void OnTestStop()
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static string CreateFileWithText(string text)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            File.WriteAllText(path + "test.txt", text);
            return path + "test.txt";
        }
    }
}