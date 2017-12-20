using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommandLine;

namespace TagCloudMakerCUI
{
    public class Option
    {
        private string inputFilePath;
        private string excludingFilePath;
        private int? fontSize;
        private int? width;
        private int? height;
        private string backColor;
        private string textColor;

        [Option('i', "inputFile", Required = true, HelpText = "Input file path.")]
        public string InputFilePath
        {
            get { return inputFilePath; }
            set
            {
                if(File.Exists(value))
                    inputFilePath = value;
            }
        }

        [Option('e', "excludingFile", Required = false, HelpText = "File with words to exclude.")]
        public string ExcludingFilePath
        {
            get { return excludingFilePath; }
            set
            {
                if(string.IsNullOrWhiteSpace(value) || File.Exists(value))
                    excludingFilePath = value;
            }
        }

        [Option('f', "fontSize", Required = true, HelpText = "Font size in pixels.")]
        public int? FontSize
        {
            get { return fontSize; }
            set { fontSize = value > 0 ? value : null; }
        }

        [Option('b', "background", Required = true, HelpText = "Background color.")]
        public string BackColor
        {
            get { return backColor; }
            set
            {
                if(IsCorrectColorName(value))
                    backColor = value;
            }
        }

        [Option('c', "textColor", Required = true, HelpText = "Text color.")]
        public string TextColor
        {
            get { return textColor; }
            set
            {
                if (IsCorrectColorName(value))
                    textColor = value;
            }
        }

        [Option('w', "width", Required = true, HelpText = "Image width.")]
        public int? Width
        {
            get { return width; }
            set { width = value > 0 ? value : null; }
        }

        [Option('h', "height", Required = true, HelpText = "Image height.")]
        public int? Height
        {
            get { return height; }
            set { height = value > 0 ? value : null; }
        }

        private bool IsCorrectColorName(string name)
        {
            if (Color.FromName(name) == Color.Black && name.ToLower() != "black")
                return false;
            return true;
        }

        public IEnumerable<string> GetUnsetParamtersNames()
        {
            var properties = GetType().GetProperties().Where(p => p.Name != "ExcludingFilePath");
            foreach (var property in properties)
            {
                if (property.GetValue(this) == null)
                    yield return property.Name;
            }
        }
    }
}