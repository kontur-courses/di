using System.Drawing;
using System.Windows.Forms;

namespace DesktopClient
{
    sealed partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Text = "TagCloud";
            
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

        #endregion
    }
}