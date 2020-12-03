﻿using System;
using System.Drawing;
using System.Linq;
using CommandLine;
using TagCloud.Settings;

namespace TagCloud.UserInterfaces
{
    public class ConsoleLineInterface
    {
        public CircularLayouterSettings LayouterSettings;
        public DrawerSettings DrawerSettings;
        public FileReaderSettings FileReaderSettings;
        public SaverSettings SaverSettings;

        public ConsoleLineInterface(string[] args)
        {
            var arguments = Parser.Default.ParseArguments<CLIArguments>(args).Value;
            
            LayouterSettings = new CircularLayouterSettings(
                new Point(arguments.CenterX, arguments.CenterY),
                arguments.SpiralPitch,
                arguments.SpiralStep);

            var fgColor = arguments.ForegroundColor != null
                ? ParseColorFromRGBString(arguments.ForegroundColor)
                : Color.FromArgb(0,0,0,0);
            DrawerSettings = new DrawerSettings(
                new Size(arguments.Width, arguments.Height), 
                arguments.Colors.Select(colorStr => ParseColorFromRGBString(colorStr)).ToList(),
                ParseColorFromRGBString(arguments.BackgroundColor),
                fgColor,
                arguments.FontFamily,
                arguments.MinFontSize, 
                arguments.MaxFontSize,
                arguments.OrderByWeight);
            
            FileReaderSettings = new FileReaderSettings(arguments.FilePath);
            
            SaverSettings = new SaverSettings(arguments.OutputPath, arguments.OutputFileName, arguments.Extention);
        }

        private Color ParseColorFromRGBString(string colorAsString)
        {
            var rgbAsArray = colorAsString.Split()
                .Select(s => Convert.ToInt32(s)).ToArray();
            if (rgbAsArray.Length != 3 || rgbAsArray.All(n => n < 0 || n > 255))
                throw new ArgumentException("Incorrect Color format");
            return Color.FromArgb(rgbAsArray[0], rgbAsArray[1], rgbAsArray[2]);
        }
    }
}