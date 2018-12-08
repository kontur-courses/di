using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagCloudApplication.Savers;

namespace TagCloudApplicationTest
{
    [TestFixture]
    public class BMPSaverShould
    {
        private BMPSaver testSaver;

        [SetUp]
        public void SetUp()
        {
            testSaver = new BMPSaver();
        }

        [Test]
        public void Save_SaveBitmapAsBMPImage()
        {
            var bm = new Bitmap(300, 300);
            var fileName = AppContext.BaseDirectory + "testImage";

            testSaver.Save(fileName, bm);

            Directory.GetFiles(AppContext.BaseDirectory).Should().ContainSingle(fn => fn == fileName);
        }
    }
}
