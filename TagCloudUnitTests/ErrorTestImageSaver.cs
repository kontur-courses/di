using System;
using System.Drawing;
using System.IO;
using System.Text;
using NUnit.Framework;
using TagCloud.ImageProcessing;

namespace TagCloudUnitTests
{
    public static class ErrorTestImageSaver
    {
        private const string DateFormat = "yyyy-MM-dd";

        private const string TimeFormat = "hh-mm-ss";

        private const string TestLogDirectoryName = "TestsLog";

        public static void SaveBitmap(Bitmap bitmap, out string fileFullPath)
        {
            fileFullPath = GetFileFullPath();

            ImageSaver.SaveBitmap(bitmap, fileFullPath);
        }

        private static string GetFileFullPath()
        {
            var testLogDirectory = ImageSaver.GetSolutionSubDirectory(TestLogDirectoryName);

            var testMethodDirectoryName = ImageSaver.GetSubDirectory(testLogDirectory,TestContext.CurrentContext.Test.MethodName);

            var fileName = GetFileName();

            return Path.Combine(testMethodDirectoryName.FullName, fileName);
        }

        private static string GetFileName()
        {
            var fileName = new StringBuilder();

            if (IsTestParameterized())
            {
                var subTestName = TestContext.CurrentContext.Test.Name;

                fileName.Append($"{subTestName}. ");
            }

            fileName.Append($"Date {DateTime.Now.ToString(DateFormat)}. Time {DateTime.Now.ToString(TimeFormat)}");

            fileName.Append(".png");

            return fileName.ToString();
        }

        private static bool IsTestParameterized()
        {
            return TestContext.CurrentContext.Test.Arguments.Length > 0;
        }
    }
}