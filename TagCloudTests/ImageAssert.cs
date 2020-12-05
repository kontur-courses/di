using System.Drawing;
using System.Security.Cryptography;
using NUnit.Framework;

namespace TagCloudTests
{
    public static class ImageAssert
    {
        public static void AreEqual(Bitmap expected, Bitmap actual, string message = "")
        {
            const string name = "AssertImage.AreEqual";
            if (expected.Size != actual.Size)
                throw new AssertionException(
                    name + " failed. Expected:<Height " + expected.Size.Height + ", Width" + expected.Size.Width +
                    ">. Actual:<Height " + actual.Size.Height + ",Width " + actual.Size.Width + ">. " +
                    message);
            var imageConverter = new ImageConverter();
            var imageExpected = new byte[1];
            imageExpected = (byte[]) imageConverter.ConvertTo(expected, imageExpected.GetType());
            var imageActual = new byte[1];
            imageActual = (byte[]) imageConverter.ConvertTo(actual, imageActual.GetType());

            var shaM = new SHA256Managed();
            var hash1 = shaM.ComputeHash(imageExpected);
            var hash2 = shaM.ComputeHash(imageActual);

            for (var i = 0; i < hash1.Length && i < hash2.Length; i++)
                if (hash1[i] != hash2[i])
                    throw new AssertionException(
                        string.Format(name + " failed. Expected:<hash value " + hash1[i] + ">. Actual:<hash value " +
                                      hash2[i] + ">. " +
                                      message));
        }
    }
}