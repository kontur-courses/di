using System.Drawing;

namespace TagsCloudGUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.but_openFile = new System.Windows.Forms.Button();
            this.box_stepSize = new System.Windows.Forms.TextBox();
            this.but_generate = new System.Windows.Forms.Button();
            this.pic_main = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.but_openExludedWordForm = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.but_fontColor = new System.Windows.Forms.Button();
            this.but_backgroundColor = new System.Windows.Forms.Button();
            this.but_save = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.box_heightPic = new System.Windows.Forms.TextBox();
            this.box_widthPic = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_main)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // but_openFile
            // 
            this.but_openFile.Location = new System.Drawing.Point(6, 41);
            this.but_openFile.Name = "but_openFile";
            this.but_openFile.Size = new System.Drawing.Size(110, 23);
            this.but_openFile.TabIndex = 0;
            this.but_openFile.Text = "Выбрать файл";
            this.but_openFile.UseVisualStyleBackColor = true;
            this.but_openFile.Click += new System.EventHandler(this.but_fileOpen_Click);
            // 
            // box_stepSize
            // 
            this.box_stepSize.Location = new System.Drawing.Point(122, 43);
            this.box_stepSize.Name = "box_stepSize";
            this.box_stepSize.Size = new System.Drawing.Size(100, 20);
            this.box_stepSize.TabIndex = 1;
            this.box_stepSize.Text = "10";
            // 
            // but_generate
            // 
            this.but_generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.but_generate.Location = new System.Drawing.Point(739, 41);
            this.but_generate.Name = "but_generate";
            this.but_generate.Size = new System.Drawing.Size(180, 23);
            this.but_generate.TabIndex = 3;
            this.but_generate.Text = "Сгенерировать изображение";
            this.but_generate.UseVisualStyleBackColor = true;
            this.but_generate.Click += new System.EventHandler(this.but_generate_Click);
            // 
            // pic_main
            // 
            this.pic_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_main.InitialImage = ((System.Drawing.Image)(resources.GetObject("pic_main.InitialImage")));
            this.pic_main.Location = new System.Drawing.Point(0, 71);
            this.pic_main.Name = "pic_main";
            this.pic_main.Size = new System.Drawing.Size(931, 494);
            this.pic_main.TabIndex = 4;
            this.pic_main.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Длина шага:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(344, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Цвет текста:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(228, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Цвет фона:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.but_openExludedWordForm);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.but_fontColor);
            this.groupBox1.Controls.Add(this.but_backgroundColor);
            this.groupBox1.Controls.Add(this.but_openFile);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.box_stepSize);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.but_generate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(931, 74);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // but_openExludedWordForm
            // 
            this.but_openExludedWordForm.Location = new System.Drawing.Point(570, 19);
            this.but_openExludedWordForm.Name = "but_openExludedWordForm";
            this.but_openExludedWordForm.Size = new System.Drawing.Size(163, 44);
            this.but_openExludedWordForm.TabIndex = 13;
            this.but_openExludedWordForm.Text = "Изменить список избегаемых слов";
            this.but_openExludedWordForm.UseVisualStyleBackColor = true;
            this.but_openExludedWordForm.Click += new System.EventHandler(this.but_openExludedWordForm_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(457, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 22);
            this.button1.TabIndex = 12;
            this.button1.Text = "Выбрать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(457, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Шрифт";
            // 
            // but_fontColor
            // 
            this.but_fontColor.Location = new System.Drawing.Point(344, 41);
            this.but_fontColor.Name = "but_fontColor";
            this.but_fontColor.Size = new System.Drawing.Size(107, 22);
            this.but_fontColor.TabIndex = 10;
            this.but_fontColor.Text = "Выбрать";
            this.but_fontColor.UseVisualStyleBackColor = true;
            this.but_fontColor.Click += new System.EventHandler(this.but_fontColor_Click);
            // 
            // but_backgroundColor
            // 
            this.but_backgroundColor.Location = new System.Drawing.Point(228, 41);
            this.but_backgroundColor.Name = "but_backgroundColor";
            this.but_backgroundColor.Size = new System.Drawing.Size(110, 22);
            this.but_backgroundColor.TabIndex = 9;
            this.but_backgroundColor.Text = "Выбрать";
            this.but_backgroundColor.UseVisualStyleBackColor = true;
            this.but_backgroundColor.Click += new System.EventHandler(this.but_backgroundColor_Click);
            // 
            // but_save
            // 
            this.but_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.but_save.Location = new System.Drawing.Point(769, 571);
            this.but_save.Name = "but_save";
            this.but_save.Size = new System.Drawing.Size(150, 23);
            this.but_save.TabIndex = 10;
            this.but_save.Text = "Сохранить изображение";
            this.but_save.UseVisualStyleBackColor = true;
            this.but_save.Click += new System.EventHandler(this.but_save_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 579);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Высота изображения:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(237, 579);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Ширина изображения:";
            // 
            // box_heightPic
            // 
            this.box_heightPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.box_heightPic.Location = new System.Drawing.Point(131, 576);
            this.box_heightPic.Name = "box_heightPic";
            this.box_heightPic.Size = new System.Drawing.Size(100, 20);
            this.box_heightPic.TabIndex = 13;
            this.box_heightPic.Text = "1080";
            // 
            // box_widthPic
            // 
            this.box_widthPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.box_widthPic.Location = new System.Drawing.Point(363, 576);
            this.box_widthPic.Name = "box_widthPic";
            this.box_widthPic.Size = new System.Drawing.Size(100, 20);
            this.box_widthPic.TabIndex = 13;
            this.box_widthPic.Text = "1920";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 601);
            this.Controls.Add(this.box_widthPic);
            this.Controls.Add(this.box_heightPic);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.but_save);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pic_main);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pic_main)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button but_openExludedWordForm;

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox box_heightPic;
        private System.Windows.Forms.TextBox box_widthPic;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Button but_backgroundColor;
        private System.Windows.Forms.Button but_fontColor;

        private System.Windows.Forms.Button but_save;

        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        #endregion

        private System.Windows.Forms.Button but_openFile;
        private System.Windows.Forms.TextBox box_stepSize;
        private System.Windows.Forms.Button but_generate;
        private System.Windows.Forms.PictureBox pic_main;
    }
}