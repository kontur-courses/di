using System;
using TagsCloudContainer.PreprocessingWorld;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var mystemUtility = new MyStemUtility(@"C:\Users\Jarvis\Documents\GitHub\di\TagsCloudContainer\TagsCloudContainer\mystem.exe");
            foreach (var str in mystemUtility.Preprocessing(new []{"мишка", "нить", "дома"}))
            {
                Console.WriteLine(str);
            }
            
        }
    }
}