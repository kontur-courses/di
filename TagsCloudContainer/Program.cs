using Autofac;


namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var scope = Configurator.GetContainer().BeginLifetimeScope();
            var creator = scope.Resolve<TagsCloudCreator>();
            creator.SetFontRandomColor();
            creator.TrySetImageFormat("png");
            creator.TrySetFontFamily("Comic Sans MS");
            creator.TrySetImageSize(500);
            creator.Create("C:\\Users\\Никита\\Desktop\\ШПОРА\\di\\TagsCloudContainer\\input.txt",
                "C:\\Users\\Никита\\Desktop\\ШПОРА\\di\\TagsCloudContainer", "Cloud");
            creator.AddStopWord("aba");
            creator.Create("C:\\Users\\Никита\\Desktop\\ШПОРА\\di\\TagsCloudContainer\\input.txt",
                "C:\\Users\\Никита\\Desktop\\ШПОРА\\di\\TagsCloudContainer", "Cloud");
        }
    }
}
