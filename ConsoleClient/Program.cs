using System.Drawing;
using Autofac;
using CommandLine;
using ConsoleClient;
using TagCloud;
using TagCloud.Abstractions;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed(o => ConfigureContainer(o).Resolve<Client>().Execute(o.Result));


IContainer ConfigureContainer(Options options)
{
    var builder = new ContainerBuilder();

    ConfigureLoader(builder, options);

    ConfigureProcessors(builder, options);

    builder.RegisterType<CountWordsTagger>().As<IWordsTagger>().SingleInstance();

    ConfigureDrawer(builder, options);

    ConfigureLayouter(builder, options);

    builder.RegisterType<DrawingCloudCreator>().As<ICloudCreator>();

    builder.RegisterType<Client>().AsSelf().SingleInstance();

    return builder.Build();
}

void ConfigureLoader(ContainerBuilder builder, Options options)
{
    var extension = Path.GetExtension(options.Source);

    switch (extension)
    {
        case ".txt":
            builder.RegisterInstance(new TxtLinesWordsLoader(options.Source)).As<IWordsLoader>();
            break;
        case ".docx" or ".doc":
            builder.RegisterInstance(new DocLinesWordsLoader(options.Source)).As<IWordsLoader>();
            break;
        default:
            throw new ArgumentException($"Extension {extension} not support");
    }
}

void ConfigureProcessors(ContainerBuilder builder, Options options)
{
    var trimToLowerProcessor = new FuncWordsProcessor(words => words.Select(w => w.Trim().ToLower()));
    builder.RegisterInstance(trimToLowerProcessor).As<IWordsProcessor>();
    
    builder.RegisterInstance(new MorphWordsProcessor(options.SelectedPartsOfSpeech)).As<IWordsProcessor>();
}

void ConfigureDrawer(ContainerBuilder builder, Options options)
{
    var drawer = new BaseCloudDrawer(new Size(options.ImageWidth, options.ImageHeight))
    {
        FontFamily = new FontFamily(options.FontFamilyName),
        MaxFontSize = options.MaxFontSize,
        MinFontSize = options.MinFontSize,
        TextColor = Color.FromName(options.TextColorName),
        BackgroundColor = Color.FromName(options.BackgroundColorName)
    };
    builder.RegisterInstance(drawer).As<ICloudDrawer>();
}

void ConfigureLayouter(ContainerBuilder builder, Options options)
{
    var pointGenerator = new SpiralPointGenerator(0.1, 0.1, options.XFlattening);
    var center = new Point(options.ImageWidth / 2, options.ImageHeight / 2);
    builder.RegisterInstance(new BaseCloudLayouter(center, pointGenerator)).As<ICloudLayouter>();
}