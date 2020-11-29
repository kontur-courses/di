using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagCloudCreator;

namespace TagCloud
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel table;
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
            for (var i = 0; i < 5; i++)
                menu.RowStyles.Add(new RowStyle(SizeType.Percent,10));
            menu.Padding = new Padding(20);
            
            
            var typeSelector = new ComboBox(){Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList};
            foreach (var layouter in layouters) typeSelector.Items.Add(layouter.Name);
            typeSelector.SelectedIndex = 0;
            
            menu.Controls.Add(typeSelector,0,0);
            
            
            var path = new TextBox(){Text = "...",Dock = DockStyle.Fill};
            var openFileButton = new Button(){Text = "Select file for usage",Dock = DockStyle.Fill, Margin = new Padding(0,0,0,100)};
            openFileButton.Click += (sender, args) =>
            {
                using OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK) path.Text = openFileDialog.FileName;
            };
            
            menu.Controls.Add(path,0,1);
            menu.Controls.Add(openFileButton,1,1);
            
            
            var drawButton = new Button() {Dock = DockStyle.Fill, Text = "Draw"};
            drawButton.Click += (sender, args) => RedrawImage();
            
            menu.Controls.Add(drawButton,0,3);

            
            var saveButton = new Button() {Dock = DockStyle.Fill, Text = "Save"};
            saveButton.Click += (sender, args) =>
            {
                using SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "png files (*.png)|*.png";
                Stream stream;
                if (saveFileDialog.ShowDialog()== DialogResult.OK)
                    if ((stream = saveFileDialog.OpenFile()) != null)
                    {
                        image.Save(stream, ImageFormat.Png);
                        stream.Close();
                    }
            };
            
            menu.Controls.Add(saveButton,0,4);
            
            return menu;
        }

        private void RedrawImage()
        {
            var path = (this.Controls[1].Controls[1] as TextBox).Text;
            (table.Controls[0] as PictureBox).Image =  CloudPrinter.DrawCloud(path);
        }
    }
}