using System.Drawing;

namespace TagsCloudContainer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.but_openFile = new System.Windows.Forms.Button();
            this.box_stepSize = new System.Windows.Forms.TextBox();
            this.comboBox_backgroundColor = new System.Windows.Forms.ComboBox();
            this.but_generate = new System.Windows.Forms.Button();
            this.pic_main = new System.Windows.Forms.PictureBox();
            this.comboBox_textColor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_main)).BeginInit();
            this.SuspendLayout();
            // 
            // but_openFile
            // 
            this.but_openFile.Location = new System.Drawing.Point(12, 26);
            this.but_openFile.Name = "but_openFile";
            this.but_openFile.Size = new System.Drawing.Size(110, 23);
            this.but_openFile.TabIndex = 0;
            this.but_openFile.Text = "Выбрать файл";
            this.but_openFile.UseVisualStyleBackColor = true;
            this.but_openFile.Click += new System.EventHandler(this.but_fileOpen_Click);
            // 
            // box_stepSize
            // 
            this.box_stepSize.Location = new System.Drawing.Point(128, 28);
            this.box_stepSize.Name = "box_stepSize";
            this.box_stepSize.Size = new System.Drawing.Size(100, 20);
            this.box_stepSize.TabIndex = 1;
            this.box_stepSize.Text = "10";
            // 
            // comboBox_backgroundColor
            // 
            this.comboBox_backgroundColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_backgroundColor.FormattingEnabled = true;
            this.comboBox_backgroundColor.Items.AddRange(new object[] { System.Drawing.Color.Black, System.Drawing.Color.White, System.Drawing.Color.Lime, System.Drawing.Color.LightBlue });
            this.comboBox_backgroundColor.Location = new System.Drawing.Point(350, 28);
            this.comboBox_backgroundColor.Name = "comboBox_backgroundColor";
            this.comboBox_backgroundColor.Size = new System.Drawing.Size(110, 21);
            this.comboBox_backgroundColor.TabIndex = 2;
            // 
            // but_generate
            // 
            this.but_generate.Location = new System.Drawing.Point(466, 26);
            this.but_generate.Name = "but_generate";
            this.but_generate.Size = new System.Drawing.Size(180, 23);
            this.but_generate.TabIndex = 3;
            this.but_generate.Text = "Сгенерировать изображение";
            this.but_generate.UseVisualStyleBackColor = true;
            this.but_generate.Click += new System.EventHandler(this.but_generate_Click);
            // 
            // pic_main
            // 
            this.pic_main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_main.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pic_main.InitialImage = ((System.Drawing.Image)(resources.GetObject("pic_main.InitialImage")));
            this.pic_main.Location = new System.Drawing.Point(0, 55);
            this.pic_main.Name = "pic_main";
            this.pic_main.Size = new System.Drawing.Size(656, 334);
            this.pic_main.TabIndex = 4;
            this.pic_main.TabStop = false;
            // 
            // comboBox_textColor
            // 
            this.comboBox_textColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_textColor.FormattingEnabled = true;
            this.comboBox_textColor.Items.AddRange(new object[] { System.Drawing.Color.Black, System.Drawing.Color.White, System.Drawing.Color.Lime, System.Drawing.Color.LightBlue });
            this.comboBox_textColor.Location = new System.Drawing.Point(234, 28);
            this.comboBox_textColor.Name = "comboBox_textColor";
            this.comboBox_textColor.Size = new System.Drawing.Size(110, 21);
            this.comboBox_textColor.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Длина шага:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Цвет текста:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(350, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Цвет фона:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 389);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_textColor);
            this.Controls.Add(this.pic_main);
            this.Controls.Add(this.but_generate);
            this.Controls.Add(this.comboBox_backgroundColor);
            this.Controls.Add(this.box_stepSize);
            this.Controls.Add(this.but_openFile);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pic_main)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        #endregion

        private System.Windows.Forms.Button but_openFile;
        private System.Windows.Forms.TextBox box_stepSize;
        private System.Windows.Forms.ComboBox comboBox_backgroundColor;
        private System.Windows.Forms.Button but_generate;
        private System.Windows.Forms.PictureBox pic_main;
        private System.Windows.Forms.ComboBox comboBox_textColor;
    }
}