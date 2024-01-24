using CommandLine;
using Microsoft.Extensions.DependencyInjection;

var options = Parser.Default.ParseArguments<Options>(args);

if (options.Errors.Any())
    return;

var appOptions = AppOptionsCreator.CreateOptions(options.Value);

var builder = new ServiceCollection();
builder.AddClient();
builder.AddDomain(appOptions.DomainOptions);
builder.AddInfrastructure();
var provider = builder.BuildServiceProvider();

var inp = options.Value.InputTextPath;
var outp = options.Value.OutputImagePath;

var text = provider.GetService<ITextLoader>()?.Load(inp);

var tagCloud = provider.GetService<ITagCloud>();
var image = tagCloud.CreateCloud(text);

provider.GetService<IImageStorage>()?.Save(image, outp);
