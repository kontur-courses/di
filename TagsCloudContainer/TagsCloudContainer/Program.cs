using System;
using System.Diagnostics;
using System.IO;
using TagsCloudContainer.PreprocessingWorld;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var mystemUtility = new MyStemUtility(@"C:\Users\Jarvis\Documents\GitHub\di\TagsCloudContainer\TagsCloudContainer\mystem.exe");
            var str = mystemUtility.GetResult(@"C:\Users\Jarvis\Documents\GitHub\di\TagsCloudContainer\TagsCloudContainer\111.txt");
            Console.WriteLine(str);
           
            Console.WriteLine("Привет. Как дела?");
        }
    }
}