// See https://aka.ms/new-console-template for more information

using TagCloud.ConsoleApp.DI;

var commandService = Configurator.ConfigureApplication();

commandService.Run();