using System;
using System.Drawing;
using System.IO;

namespace TagsCloudContainer.UI
{
    public class SetVisualizationSettingsAction : ConsoleUiAction
    {
        public override string Category => "Visualization";
        public override string Name => "SetVisualizationSettings";
        public override string Description { get; }

        public SetVisualizationSettingsAction
            (TextReader reader, TextWriter writer) : base(reader, writer)
        {
        }

        public override void Perform()
        {
            writer.WriteLine("Change visualizator settings");
            SetSize();
        }

        private void SetSize()
        {
            var size = new Size();
            writer.WriteLine("Set Width of result image");
            while (true)
            {
                var width = reader.ReadLine();
                if (int.TryParse(width, out var w))
                {
                    size.Width = w;
                    break;
                }
                writer.WriteLine("Width should be int!");
            }

            writer.WriteLine("Set Height of result image");
            while (true)
            {
                var height = reader.ReadLine();
                if (int.TryParse(height, out var h))
                {
                    size.Height = h;
                    break;
                }
                writer.WriteLine("Width should be int!");
            }
            AppSettings.ImageSize = size;
            ShouldContinue(SetBackground);
        }

        private void SetBackground()
        {
            writer.WriteLine("Set Color image background");
            while (true)
            {
                try
                {
                    var clr = reader.ReadLine();
                    AppSettings.BackgroundColor = Color.FromName(clr);
                    ShouldContinue(SetFontFamily);
                    return;
                }
                catch (Exception)
                {
                    writer.WriteLine("It is not a color, try set 'Red' color");
                }
            }
            
        }

        private void SetFontFamily()
        {
            writer.WriteLine("Set FontFamily of Tags");
            while (true)
            {
                try
                {
                    var family = reader.ReadLine();
                    AppSettings.FontFamily = new FontFamily(family);
                    ShouldContinue(SetMinMargin);
                    return;
                }
                catch (Exception)
                {
                    writer.WriteLine("It is not a FontFamily, try set 'Arial' family");
                }
            }
        }

        private void SetMinMargin()
        {
            writer.WriteLine("Set minimal margin of cloud from borders");
            while (true)
            {
                var width = reader.ReadLine();
                if (float.TryParse(width, out var margin))
                {
                    AppSettings.MinMargin = margin;
                    break;
                }
                writer.WriteLine("margin should be float!");
            }
            ShouldContinue(SetFillingTags);
        }

        private void SetFillingTags()
        {
            writer.WriteLine("Set Filling Tag rectangles by colors, yes or no? 'y', 'n'");
            while (true)
            {
                var answer = reader.ReadLine();
                switch (answer)
                {
                    case "y":
                        AppSettings.FillTags = true;
                        return;
                    case "n":
                        AppSettings.FillTags = false;
                        return;
                    default:
                        writer.WriteLine("Answer should be 'y' or 'n'");
                        break;
                }
            }
        }

        private void ShouldContinue(Action action)
        {
            writer.WriteLine("Should continue change settings, yes or no? 'y', 'n'");
            while (true)
            {
                var answer = reader.ReadLine();
                switch (answer)
                {
                    case "y":
                        AppSettings.FillTags = true;
                        action();
                        return;
                    case "n":
                        AppSettings.FillTags = false;
                        return;
                    default:
                        writer.WriteLine("Answer should be 'y' or 'n'");
                        break;
                }
            }
        }
    }
}