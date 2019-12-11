using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;
using TagsCloudContainer.Core;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.UserInterface.ArgumentsParsing;

namespace TagsCloudContainer.UserInterface.Window
{
    public sealed class ApplicationForm : MetroForm, IUserInterface, IResultDisplay
    {
        private readonly IArgumentsParser<UserInterfaceArguments> argumentsParser;
        private Action<Parameters> runProgramAction;

        private MetroTextBox inputFilePathTextBox;
        private MetroTextBox outputFileTextBox;
        private NumericUpDown widthSetter;
        private NumericUpDown heightSetter;
        private ComboBox fontSelector;
        private ComboBox formatSelector;
        private MetroTextBox colorSelector;

        public ApplicationForm(IArgumentsParser<UserInterfaceArguments> argumentsParser)
        {
            this.argumentsParser = argumentsParser;
            Size = new Size(800, 600);
            Text = @"TagsCloudContainer";
            ShadowType = MetroFormShadowType.None;
            Controls.Add(InitTable());
        }

        public void Run(string[] programArgs, Action<Parameters> runProgram)
        {
            runProgramAction = runProgram;
            Application.Run(this);
        }

        public void ShowResult(Bitmap bitmap)
        {
            using (var bitmapForm = new BitmapForm(bitmap))
            {
                bitmapForm.ShowDialog();
            }
        }

        private TableLayoutPanel InitTable()
        {
            var table = new TableLayoutPanel();
            var controls = InitControls();
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));

            table.AddControlsToRows(controls, 1, 0, SizeType.Absolute, 40);
            table.AddControls(
                controls.Select(c => c.Name).Select(Elements.GetLabel).ToList(),
                0, 0);
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            table.Controls.Add(GetPerformButton(), 0, 7);
            table.Controls.Add(GetOpenFileButton(), 2, 0);

            table.Dock = DockStyle.Fill;
            return table;
        }

        private List<Control> InitControls()
        {
            inputFilePathTextBox = new MetroTextBox {Name = "Путь к входному файлу"};
            outputFileTextBox = new MetroTextBox {Name = "Путь к выходному файлу", Text = @"test.png"};
            widthSetter = new NumericUpDown {Name = "Ширина результата", Maximum = 2000, Minimum = 1, Value = 800};
            heightSetter = new NumericUpDown {Name = "Высота результата", Maximum = 2000, Minimum = 1, Value = 600};
            fontSelector = Elements.TypeBox(FontFamily.Families.Select(f => f.Name), "Arial", "Используемый шрифт");
            formatSelector = Elements.TypeBox(
                typeof(ImageFormat).GetProperties().Select(p => p.Name).Where(name => name != "Guid"),
                "Png",
                "Формат результата");
            colorSelector = new MetroTextBox {Name = "Используемые цвета", Text = @"Aqua Black"};
            return new List<Control>
            {
                inputFilePathTextBox, outputFileTextBox, widthSetter, heightSetter, fontSelector, formatSelector,
                colorSelector
            };
        }

        private Button GetPerformButton()
        {
            var performButton = new MetroButton
            {
                Text = @"Показать облако",
                Dock = DockStyle.Bottom
            };

            performButton.Click += (sender, args) =>
            {
                var colors = colorSelector.Text.Split(' ').ToList();
                var arguments = new UserInterfaceArguments(inputFilePathTextBox.Text,
                    outputFileTextBox.Text, (int) widthSetter.Value, (int) heightSetter.Value,
                    fontSelector.Text, colors, formatSelector.Text);
                var parameters = argumentsParser.ParseArgumentsToParameters(arguments);
                runProgramAction(parameters);
            };
            return performButton;
        }

        private Button GetOpenFileButton()
        {
            var openFileButton = new MetroButton
            {
                Text = @"Открыть",
                Dock = DockStyle.Fill
            };

            openFileButton.Click += (sender, args) =>
            {
                using (var fileDialog = new OpenFileDialog())
                {
                    fileDialog.Filter = @"Текстовые файлы (*.txt)|*.txt";

                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        inputFilePathTextBox.Text = fileDialog.FileName;
                    }
                }
            };

            return openFileButton;
        }
    }
}