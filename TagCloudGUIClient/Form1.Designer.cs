using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CloudLayouters;
using TagCloudCreator;

namespace TagCloud
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel table;
        private BaseCloudLayouter layouter;
        private Size imageSizeContainer;
        private Size imageSize
        {
            get => imageSizeContainer;
            set
            {
                image = new Bitmap(value.Width,value.Height);
                imageSizeContainer = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 750);
            this.Text = "Form1";
            table.Controls.Add(new PictureBox(){Image = image,Dock = DockStyle.Fill,SizeMode = PictureBoxSizeMode.Zoom},0,0);
            table.Controls.Add(GetMenu(),1,0);
            Controls.Add(table);
        }

        private TableLayoutPanel GetMenu()
        {
            var menu = new TableLayoutPanel(){Dock = DockStyle.Fill};
            menu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,80));
            menu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,20));
            for (var i = 0; i < 7; i++)
                menu.RowStyles.Add(new RowStyle(SizeType.Percent,10));
            menu.Padding = new Padding(20);
            
            
            var typeSelector = new ComboBox(){Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList};
            foreach (var layouter in layouters) typeSelector.Items.Add(layouter.Name);
            typeSelector.SelectedIndex = 0;
            typeSelector.SelectedIndexChanged += (sender, args) =>
                layouter = layouters.First(x => x.Name == (string) typeSelector.SelectedItem);
            
            menu.Controls.Add(typeSelector,0,0);
            
            
            var path = new TextBox(){Text = "...",Dock = DockStyle.Fill};
            var openFileButton = new Button()
            {
                Text = "Select file for usage",
                Dock = DockStyle.Fill, 
            };
            openFileButton.Click += (sender, args) =>
            {
                using OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "files with text|*.txt;*.doc;*.docx;*.html;*.pdf;*.md";
                if (openFileDialog.ShowDialog() == DialogResult.OK) path.Text = openFileDialog.FileName;
            };
            
            menu.Controls.Add(path,0,1);
            menu.Controls.Add(openFileButton,1,1);
            
            
            
            var fontSelector = new ComboBox(){DropDownStyle = ComboBoxStyle.DropDownList,Dock = DockStyle.Fill};
            fontSelector.Text = "select font";
            fontSelector.Items.AddRange(FontFamily.Families.Select(x=>x.Name).ToArray());
            fontSelector.Name = "fontSelector";
            menu.Controls.Add(fontSelector,0,2);
            
            
            var sizeSelector = new ComboBox(){DropDownStyle = ComboBoxStyle.DropDownList,Dock = DockStyle.Fill};
            sizeSelector.Text = "select size";
            sizeSelector.SelectedIndexChanged += (sender, args) =>
            {
                var pair = ((string) sizeSelector.SelectedItem).Split('x');
                imageSize = new Size(int.Parse(pair[0]),int.Parse(pair[1]));
                layouters.ForEach(x=>x.Center = new Point(imageSize.Width/2,image.Height/2));
            };
            sizeSelector.Items.AddRange(new []{"3840x2160","1920x1080","1280x800","800x600"});
            menu.Controls.Add(sizeSelector,0,3);
            
            
            var colorSelectorSelector = new ComboBox(){DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill};
            colorSelectorSelector.Text = "Select color selector";
            colorSelectorSelector.Items.AddRange(colorSelectors.Select(x => x.Name).ToArray());
            menu.Controls.Add(colorSelectorSelector,0,4);
            
            
            var drawButton = new Button() {Dock = DockStyle.Fill, Text = "Draw"};
            drawButton.Click += (sender, args) => RedrawImage();
            
            menu.Controls.Add(drawButton,0,5);

            
            var saveButton = new Button() {Dock = DockStyle.Fill, Text = "Save"};
            saveButton.Click += (sender, args) =>
            {
                using SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "png files (*.png)|*.png";
                Stream stream;
                if (saveFileDialog.ShowDialog()== DialogResult.OK)
                    if ((stream = saveFileDialog.OpenFile()) != null)
                    {
                        (table.Controls[0] as PictureBox).Image.Save(stream, ImageFormat.Png);
                        stream.Close();
                    }
            };
            
            menu.Controls.Add(saveButton,0,6);
            
            return menu;
        }

        private void RedrawImage()
        {
            var fontFamily = FontFamily.Families.First(x =>
                x.Name == (string) (Controls[0].Controls[1].Controls[3] as ComboBox).SelectedItem);
            var colorSelector = colorSelectors.First(x =>
                x.Name == (string) (Controls[0].Controls[1].Controls[5] as ComboBox).SelectedItem);
            var path = (this.Controls[0].Controls[1].Controls[1] as TextBox).Text;
            (table.Controls[0] as PictureBox).Image = 
                cloudPrinter.DrawCloud(path,
                    layouter,
                    imageSize ,
                    fontFamily,
                    colorSelector);
        }
    }
}