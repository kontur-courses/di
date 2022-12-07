using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudVisualization.Drawer;

public class CloudForm : Form
{

    private readonly ICloudDrawer cloudDrawer;
    private readonly string path = "result//result.png";
    private readonly RectangleF[] rectangles;


    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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

    public CloudForm(Size formSize, RectangleF[] rectangles,
        ICloudDrawer cloudDrawer)
    {
        InitializeComponent(formSize);
        this.rectangles = rectangles;
        this.cloudDrawer = cloudDrawer;
    }


    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        var random = new Random();
        for (var i = 0; i < rectangles.Length; i++)
        {
            var word = new StringBuilder();
            var length = random.Next(5, 10);
            for (var j = 0; j < length; j++)
            {
                word.Append(letters[random.Next(0, letters.Length - 1)]);
            }

            g.DrawString(word.ToString(),
                SystemFonts.DialogFont,
                new SolidBrush(Color.FromArgb(random.Next(0, 255),
                    random.Next(0, 255),
                    random.Next(0, 255))),
                rectangles[i]);
        }

        using (var bitmap = new Bitmap(Size.Width, Size.Height))
        {
            var image = Graphics.FromImage(bitmap);
            for (var i = 0; i < rectangles.Length; i++)
            {
                var word = new StringBuilder();
                var length = random.Next(5, 10);
                for (var j = 0; j < length; j++)
                {
                    word.Append(letters[random.Next(0, letters.Length - 1)]);
                }

                image.DrawString(word.ToString(),
                    SystemFonts.DialogFont,
                    new SolidBrush(Color.FromArgb(random.Next(0, 255),
                        random.Next(0, 255),
                        random.Next(0, 255))),
                    rectangles[i]);
            }


            Directory.CreateDirectory("result");
            bitmap.Save(path, ImageFormat.Png);
        }

    }

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent(Size size)
    {
        MaximizeBox = false;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = size;
        this.Text = "Form1";
    }
}