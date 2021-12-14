using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.WordProcessors;
using IContainer = Autofac.IContainer;

namespace DesktopClient
{
    public sealed partial class MainForm : Form
    {
        private readonly PictureBox cloudBox;
        private readonly Button setIgnoreFileButton;
        private readonly Button fontButton;
        private readonly Button colorButton;
        private readonly NumericUpDown wordCountToStatistic;
        private readonly NumericUpDown density;
        private readonly NumericUpDown minWordLengthToStatistic;

        private readonly CheckBox isLiteraryText;
        private readonly CheckBox randomColor;
        private Color? color;
        
        private string font = "Arials";
        private string? textFilePath;
        private string ignoreWordsFileName = @"C:\Users\Max\Desktop\ignore.txt";

        private IContainer container;

        private bool changed;

        public MainForm()
        {
            InitializeComponent();
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            
            cloudBox = new PictureBox
            {
                Width = 720,
                Height = 720,
                BackColor = Color.Bisque
            };

            wordCountToStatistic = new NumericUpDown
            {
                Location = new Point(720, 200),
                Size = new Size(150, 50)
            };
            Controls.Add(new Label
            {
                Text = "Word count to statistic", 
                Size = wordCountToStatistic.Size, 
                Location = new Point(wordCountToStatistic.Location.X + wordCountToStatistic.Size.Width, wordCountToStatistic.Location.Y)
            });
            wordCountToStatistic.ValueChanged += (_, _) =>
            {
                changed = true; Invalidate(); 
            };
            
            density = new NumericUpDown
            {
                Location = new Point(720, 150),
                Size = new Size(150, 50),
                Value = 5
            };
            Controls.Add(new Label
            {
                Text = "Density", 
                Size = density.Size, 
                Location = new Point(density.Location.X + density.Size.Width, density.Location.Y)
            });
            density.ValueChanged += (_, _) =>
            {
                changed = true; Invalidate(); 
            };

            minWordLengthToStatistic = new NumericUpDown
            {
                Location = new Point(720, 100),
                Size = new Size(150, 50),
                Text = "Min word length"
            };
            Controls.Add(new Label
            {
                Text = "Min word length", 
                Size = minWordLengthToStatistic.Size, 
                Location = new Point(minWordLengthToStatistic.Location.X + minWordLengthToStatistic.Size.Width, minWordLengthToStatistic.Location.Y)
            });
            minWordLengthToStatistic.ValueChanged += (_, _) => 
            {
                changed = true; Invalidate(); 
            };

            isLiteraryText = new CheckBox
            {
                Location = new Point(720, 50),
                Size = new Size(150, 50),
                Text = "Literary text"
            };
            isLiteraryText.Checked = true;
            isLiteraryText.CheckedChanged += (_, _) => 
            {
                changed = true; Invalidate(); 
            };
            
            randomColor = new CheckBox
            {
                Location = new Point(1120, 50),
                Size = new Size(150, 50),
                Text = "Random color"
            };
            randomColor.Checked = true;
            randomColor.CheckedChanged += (_, _) => 
            {
                changed = true; Invalidate(); 
            };
            
            setIgnoreFileButton = new Button
            {
                Location = new Point(720, 0),
                Size = new Size(150, 50),
                Text = "Set ignore words"
            };
            setIgnoreFileButton.Click += (_, _) =>
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "(txt)|*txt";
                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                ignoreWordsFileName = openFileDialog.FileName;
                changed = true; Invalidate(); 
            };
            
            fontButton = new Button
            {
                Location = new Point(920, 0),
                Size = new Size(150, 50),
                Text = "Change font"
            };
            fontButton.Click += (_, _) =>
            {
                var fontDialog = new FontDialog();
                if (fontDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                font = fontDialog.Font.Name;
                changed = true; Invalidate(); 
            };
            
            colorButton = new Button
            {
                Location = new Point(1120, 0),
                Size = new Size(150, 50),
                Text = "Change color"
            };
            colorButton.Click += (_, _) =>
            {
                var colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                color = colorDialog.Color;
                changed = true; Invalidate(); 
            };
            
            
            Controls.Add(setIgnoreFileButton);
            Controls.Add(fontButton);
            Controls.Add(colorButton);
            Controls.Add(cloudBox);
            Controls.Add(wordCountToStatistic);
            Controls.Add(density);
            Controls.Add(minWordLengthToStatistic);
            Controls.Add(isLiteraryText);
            Controls.Add(randomColor);
            randomColor.Checked = true;
            
            AllowDrop = true;
            cloudBox.AllowDrop = true;
            cloudBox.DragEnter += CloudDragEnter;
            cloudBox.DragDrop += CloudDragDrop;
        }

        private void CloudDragEnter(object sender, DragEventArgs e) 
        {
            e.Effect = DragDropEffects.Move;
        }

        private void CloudDragDrop(object sender, DragEventArgs e) 
        {
            textFilePath = ((string[]) e.Data.GetData(DataFormats.FileDrop, false)).First();
            changed = true;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (textFilePath is null || !changed) return;
            cloudBox.Image = CreateTags();
            changed = false;
        }

        private Config GetConfig()
        {
            var config = new Config
            {
                Color = randomColor.Checked ? null : color,
                WordCountToStatistic = wordCountToStatistic.Value > 0 ? (int)wordCountToStatistic.Value : -1,
                Density = density.Value > 0 ? (double)density.Value : 1d,
                MinWordToStatisticLength = (byte)minWordLengthToStatistic.Value,
                Font = font,
                TextFilePath = textFilePath!,
                CustomIgnoreFilePath = ignoreWordsFileName,
                TagCloudResultActions = TagCloudResultActions.None,
                SourceTextInterpretationMode = isLiteraryText.Checked ? SourceTextInterpretationMode.LiteraryText : SourceTextInterpretationMode.OneWordPerLine
            };

            return config;
        }
        
        private Bitmap CreateTags()
        {
            var config = GetConfig();
            container = Configurator.InjectWith(config);
            var painter = container.BeginLifetimeScope().Resolve<IPrinter<Text>>();
            return painter.GetBitmap(GetRectangles(), new Size(700, 700));
        }

        private IEnumerable<Text> GetRectangles()
        {
            using var scope = container.BeginLifetimeScope();
            var layouter = scope.Resolve<ILayouter<Rectangle>>();
            var statistic = scope.Resolve<IWordsStatistics>();
            var format = TextFormat.GetFormatByExtension(Path.GetExtension(textFilePath));
            var reader = scope.Resolve<IEnumerable<IFileReader>>().FirstOrDefault(x => x.Format == format) ?? throw new ArgumentException("no proper reader exist");
            var text = scope.Resolve<ITextProcessor>().ProcessWords(scope.Resolve<ITextParser>().ParseText(reader.ReadFile(textFilePath!)));
            statistic.AddWords(text);
            foreach (var info in scope.Resolve<IWordStatisticsToSizeConverter>().Convert(statistic, wordCountToStatistic.Value > 0 ? (int)wordCountToStatistic.Value : -1))
                yield return new Text(info.Word, font, layouter.PutNextRectangle(info.GetCollisionSize()));
        }
    }
}