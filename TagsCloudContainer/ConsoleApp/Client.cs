using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Autofac;
using TagsCloudVisualization;
using TagsCloudVisualization.Factories;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.TextHandlers;
using TagsCloudVisualization.TextPreparers;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.Visualization.Configurator;

namespace ConsoleApp
{
    public class Client
    {
        private ScreenConfig screenConfig;
        private List<Func<string, string>> wordsPreparers;
        private List<Func<string, bool>> wordsFilters;
        private int fontSize;
        private Func<string, Color> colorizer;
        private Point center;
        private string wordsPath;
        private string imageName = "TagCloud";
        private ImageFormat imageFormat = ImageFormat.Png;

        public void CreateCloud()
        {
            SetParamsToDefault();
            
            HandleCommands();

            CreateAndSaveImage();
        }

        private void HandleCommands()
        {
            while (true)
            {
                if (!TryReadConsoleInput(out var consoleInput))
                {
                    Console.WriteLine("Wrong input");
                    continue;
                }

                if (consoleInput is "default")
                {
                    SetParamsToDefault();
                    break;
                }
                
                if (consoleInput is "finish" && TryFinish())
                {
                    break;
                }

                var result = consoleInput switch
                {
                    "image_size" => TryConfigureImageSize(),
                    "bg_color" => TryConfigureBackgroundColor(),
                    "words_color" => TryConfigureWordsColor(),
                    "font_size" => TryConfigureFontSize(),
                    "center" => TryConfigureCenter(),
                    "image_name" => TryConfigureImageName(),
                    "image_format" => TryConfigureImageFormat(),
                    "words_path" => TryConfigureWordsPath(),
                    _ => false
                };

                if (result)
                {
                    Console.WriteLine(consoleInput + " was configured successfully");
                }
                else
                {
                    Console.WriteLine(consoleInput + " wasn't configured or incorrect command name");
                }
            }
            
            ConfigurePreparations();
            ConfigureFilters();
        }

        private void CreateAndSaveImage()
        {
            var container = RegisterDependencies();

            var creator = container.Resolve<ITagCloudCreator>();
            
            container.Dispose();

            var img = creator.CreateFromFile(wordsPath);

            img.Save(imageName + "." + imageFormat.ToString().ToLower(), ImageFormat.Png);
        }

        private IContainer RegisterDependencies()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Spiral>().As<IPointPlacer>();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            containerBuilder.RegisterType<WordsVisualizer>().As<IVisualizer>();
            containerBuilder.RegisterType<WordsVisualizingTokenFactory>().As<IVisualizingTokenFactory>();
            containerBuilder.RegisterType<WordsVisualizingConfigurator>().As<IVisualizingConfigurator>();
            containerBuilder.RegisterType<TxtParser>().As<IParser>();
            containerBuilder.RegisterType<TextPreparer>().As<ITextPreparer>();
            containerBuilder.RegisterType<TextHandler>().As<ITextHandler>();
            containerBuilder.RegisterType<TagCloudCreator>().As<ITagCloudCreator>();

            containerBuilder.Register(_ => screenConfig).As<ScreenConfig>();
            containerBuilder.Register(_ => center).As<Point>();
            containerBuilder.Register(_ => fontSize).As<int>();
            containerBuilder.RegisterInstance(colorizer);
            containerBuilder.RegisterInstance(wordsPreparers).As<IEnumerable<Func<string, string>>>();
            containerBuilder.RegisterInstance(wordsFilters).As<IEnumerable<Func<string, bool>>>();

            return containerBuilder.Build();
        }

        private bool TryConfigureWordsPath()
        {
            Console.Write("Input words file path: ");
            if (!TryReadConsoleInput(out var consoleInput))
            {
                return false;
            }

            if (!File.Exists(consoleInput))
            {
                Console.WriteLine("Invalid path");
                return false;
            }
            
            wordsPath = consoleInput;
            return true;
        }

        private bool TryConfigureImageName()
        {
            Console.Write("Input image name: ");
            if (!TryReadConsoleInput(out var consoleInput))
            {
                return false;
            }

            imageName = consoleInput;
            return true;
        }
        
        private bool TryConfigureImageFormat()
        {
            Console.Write("Input image format: ");
            if (!TryReadConsoleInput(out var consoleInput))
            {
                return false;
            }

            var format = consoleInput.ToLower();

            switch (format)
            {
                case "png": 
                    imageFormat = ImageFormat.Png;
                    return true;
                case "jpg":
                case "jpeg":
                    imageFormat = ImageFormat.Jpeg;
                    return true;
                case "bmp":
                    imageFormat = ImageFormat.Bmp;
                    return true;
                case "tiff":
                    imageFormat = ImageFormat.Tiff;
                    return true;
                case "exif":
                    imageFormat = ImageFormat.Exif;
                    return true;
                default:
                    Console.WriteLine("Wrong format, should be one of this: png, jpg, jpeg, bmp, tiff, exif");
                    return false;
            }
        }

        private bool TryConfigureImageSize()
        {
            Console.Write("Input cloud image size (two integers separated by space): ");

            if (!TryReadConsoleInput(out var consoleInput))
            {
                return false;
            }

            var sizeArray = consoleInput.Split(' ').ToArray();

            if (sizeArray.Length != 2 ||
                !int.TryParse(sizeArray[0], out var width) ||
                !int.TryParse(sizeArray[1], out var height) || 
                width <= 0 || height <= 0)
            {
                Console.WriteLine("Image size should be two positive integers separated by space");
                return false;
            }
            
            var size = new Size(width, height);

            screenConfig.Size = size;
            return true;
        }

        private bool TryConfigureBackgroundColor()
        {
            Console.Write("Input background color as hex: ");

            if (!TryReadConsoleInput(out var consoleInput))
            {
                return false;
            }

            var color = ColorTranslator.FromHtml(consoleInput);

            screenConfig.BackgroundColor = color;

            return true;
        }

        private bool TryConfigureWordsColor()
        {
            Console.Write("Input words color as hex: ");

            if (!TryReadConsoleInput(out var consoleInput))
            {
                return false;
            }

            var color = ColorTranslator.FromHtml(consoleInput);

            colorizer = _ => color;

            return true;
        }

        private bool TryConfigureFontSize()
        {
            Console.Write("Input font size: ");

            if (!TryReadConsoleInput(out var consoleInput))
            {
                return false;
            }
            
            if (!int.TryParse(consoleInput, out var parsedSize) || parsedSize <= 0)
            {
                Console.WriteLine("Font size should be positive integer");
                return false;
            }

            fontSize = parsedSize;

            return true;
        }

        private void ConfigurePreparations()
        {
            wordsPreparers.Add(s => s.ToLower());
        }

        private void ConfigureFilters()
        {
            wordsFilters.Add(s => s.Length < 3);    
        }

        private bool TryConfigureCenter()
        {
            Console.Write("Input center position (two integers separated by space): ");

            if (!TryReadConsoleInput(out var consoleInput))
            {
                return false;
            }

            var centerCoords = consoleInput.Split(' ').ToArray();

            if (centerCoords.Length != 2 ||
                !int.TryParse(centerCoords[0], out var x) ||
                !int.TryParse(centerCoords[1], out var y) || 
                x <= 0 || y <= 0)
            {
                Console.WriteLine("Center should be two positive integers separated by space");
                return false;
            }

            center = new Point(x, y);

            return true;
        }

        private void SetParamsToDefault()
        {
            screenConfig = new ScreenConfig {BackgroundColor = Color.White, Size = new Size(800, 600)};
            center = new Point(400, 300);
            fontSize = 11;
            colorizer = _ => Color.Black;
            wordsPreparers = new List<Func<string, string>>();
            wordsFilters = new List<Func<string, bool>>();
        }

        private bool TryFinish()
        {
            if (wordsPath is null)
            {
                Console.WriteLine("You need to configure words_path to finish");
                return false;
            }

            return true;
        }

        private static bool TryReadConsoleInput(out string consoleInput)
        {
            consoleInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(consoleInput))
            {
                Console.WriteLine("Wrong input!");
                return false;
            }

            return true;
        }
    }
}