using Autofac;
using TagCloudContainer.Configs;
using TagCloudContainer.Core;
using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Core.Models;
using TagCloudContainer.Core.Utils;
using TagCloudContainer.Forms.Interfaces;
using TagCloudContainer.Forms.Validators;

namespace TagCloudContainer.Forms;

public static class Register
{
    public static IContainer Registry()
    {
        var builder = new ContainerBuilder();
        IContainer container;

        builder.RegisterType<TagCloud>();
        builder.RegisterType<Settings>();

        var tagCloudContainerConfig = GetTagCloudContainerConfigWithDefaultPropertiesValues();

        builder.RegisterInstance(tagCloudContainerConfig).As<ITagCloudContainerConfig>();
        builder.RegisterInstance(tagCloudContainerConfig).As<ITagCloudFormConfig>();

        builder.RegisterType<TagCloudContainerConfigValidator>().As<IConfigValidator<ITagCloudContainerConfig>>();
        builder.RegisterType<TagCloudFormConfigValidator>().As<IConfigValidator<ITagCloudFormConfig>>();
        builder.RegisterType<ImageCreator>().As<IImageCreator>().SingleInstance();
        builder.RegisterType<SizeInvestigator>().As<ISizeInvestigator>().SingleInstance();
        builder.RegisterType<WordValidator>().As<IWordValidator>().SingleInstance();
        builder.RegisterType<TagCloudPlacer>().As<ITagCloudPlacer>().SingleInstance();
        builder.RegisterType<WordsReader>().As<IWordsReader>().SingleInstance();
        builder.RegisterType<TagCloudProvider>().As<ITagCloudProvider>().SingleInstance();
            
        try
        {
            container = builder.Build();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return container;
    }

    private static ITagCloudContainerConfig GetTagCloudContainerConfigWithDefaultPropertiesValues()
    {
        return new TagCloudContainerConfig()
        {
            Random = true,
            StandartSize = new Size(10, 10),
            ImageSize = Screens.Sizes.First(),
            Center = new Point(1, 1),
            NearestToTheCenterPoints = new SortedList<float, Point>(),
            PutRectangles = new List<Rectangle>(),
            FilePath = Path.Combine(TagCloudContainerConfig.GetMainDirectoryPath(), "words.txt"),
            ExcludeWordsFilePath = Path.Combine(TagCloudContainerConfig.GetMainDirectoryPath(), "boring_words.txt"),
            MainDirectoryPath = TagCloudContainerConfig.GetMainDirectoryPath(),
            ImageName = "TagCloudResult.png",
            NeedValidate = true,
            FontFamily = "Arial",
            Color = Colors.GetAll().First().Value,
            BackgroundColor = Colors.GetAll().First().Value,
        };
    }
}