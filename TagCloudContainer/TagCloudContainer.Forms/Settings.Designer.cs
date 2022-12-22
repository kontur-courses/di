using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;

namespace TagCloudContainer.Forms;

partial class Settings
{
    private IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.resultButton = new System.Windows.Forms.Button();
        this.Colors = new System.Windows.Forms.ComboBox();
        this.BackgroundColors = new System.Windows.Forms.ComboBox();
        this.Fonts = new System.Windows.Forms.ComboBox();
        this.ColorText = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.Sizes = new System.Windows.Forms.ComboBox();
        this.label3 = new System.Windows.Forms.Label();
        this.button1 = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // resultButton
        // 
        this.resultButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
        this.resultButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        this.resultButton.Location = new System.Drawing.Point(21, 213);
        this.resultButton.Name = "resultButton";
        this.resultButton.Size = new System.Drawing.Size(282, 43);
        this.resultButton.TabIndex = 0;
        this.resultButton.Text = "Запустить со случ. значениям";
        this.resultButton.UseVisualStyleBackColor = false;
        this.resultButton.Click += new System.EventHandler(this.randomStart_Click);
        // 
        // Colors
        // 
        this.Colors.FormattingEnabled = true;
        this.Colors.Location = new System.Drawing.Point(21, 44);
        this.Colors.Items.AddRange(Additions.Models.Colors.GetAll().Keys.ToArray());
        this.Colors.Name = "Colors";
        this.Colors.Size = new System.Drawing.Size(149, 28);
        this.Colors.TabIndex = 1;
        this.Colors.Text = "Black";
        // 
        // BackgroundColors
        // 
        this.BackgroundColors.FormattingEnabled = true;
        this.BackgroundColors.Location = new System.Drawing.Point(193, 44);
        this.BackgroundColors.Items.AddRange(Additions.Models.Colors.GetAll().Keys.ToArray());
        this.BackgroundColors.Name = "BackgroundColors";
        this.BackgroundColors.Size = new System.Drawing.Size(153, 28);
        this.BackgroundColors.TabIndex = 2;
        this.BackgroundColors.Text = "White";
        // 
        // Fonts
        // 
        this.Fonts.FormattingEnabled = true;
        var fonts = new InstalledFontCollection().Families.Select(f => f.Name);
        this.Fonts.Items.AddRange(fonts.ToArray());
        this.Fonts.Location = new System.Drawing.Point(369, 44);
        this.Fonts.Name = "Fonts";
        this.Fonts.Size = new System.Drawing.Size(155, 28);
        this.Fonts.TabIndex = 3;
        this.Fonts.Text = "Arial";
        // 
        // ColorText
        // 
        this.ColorText.AutoSize = true;
        this.ColorText.Location = new System.Drawing.Point(21, 9);
        this.ColorText.Name = "ColorText";
        this.ColorText.Size = new System.Drawing.Size(149, 20);
        this.ColorText.TabIndex = 4;
        this.ColorText.Text = "Выберите цвет слов";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(193, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(153, 20);
        this.label1.TabIndex = 5;
        this.label1.Text = "Выберите цвет фона";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(378, 9);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(128, 20);
        this.label2.TabIndex = 6;
        this.label2.Text = "Выберите шрифт";
        // 
        // Sizes
        // 
        this.Sizes.FormattingEnabled = true;
        this.Sizes.Location = new System.Drawing.Point(544, 44);
        this.Sizes.Items.AddRange(Additions.Models.Screens.Sizes);
        this.Sizes.Name = "Sizes";
        this.Sizes.Size = new System.Drawing.Size(155, 28);
        this.Sizes.TabIndex = 9;
        this.Sizes.Text = Additions.Models.Screens.Sizes.First();
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(556, 9);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(134, 20);
        this.label3.TabIndex = 10;
        this.label3.Text = "Выберите размер";
        // 
        // button1
        // 
        this.button1.BackColor = System.Drawing.SystemColors.InactiveCaption;
        this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        this.button1.Location = new System.Drawing.Point(360, 213);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(282, 43);
        this.button1.TabIndex = 11;
        this.button1.Text = "Запустить с большими в цетре";
        this.button1.UseVisualStyleBackColor = false;
        this.button1.Click += new System.EventHandler(this.biggerInCenter_Click);
        // 
        // Settings
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(722, 268);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.Sizes);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.ColorText);
        this.Controls.Add(this.Fonts);
        this.Controls.Add(this.BackgroundColors);
        this.Controls.Add(this.Colors);
        this.Controls.Add(this.resultButton);
        this.Name = "Settings";
        this.Text = "Tag cloud settings";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private Button resultButton;
    private CheckBox checkBox1;
    private ComboBox Colors;
    private ComboBox BackgroundColors;
    private ComboBox Fonts;
    private Label ColorText;
    private Label label1;
    private Label label2;
    private ComboBox Sizes;
    private Label label3;
    private Button button1;
}
