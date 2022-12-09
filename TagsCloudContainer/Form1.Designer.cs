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
            this.but_openFile = new System.Windows.Forms.Button();
            this.box_stepSize = new System.Windows.Forms.TextBox();
            this.comboBox_backgroundColor = new System.Windows.Forms.ComboBox();
            this.but_generate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox_textColor = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // 
            // comboBox_backgroundColor
            // 
            this.comboBox_backgroundColor.FormattingEnabled = true;
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
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(656, 334);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // comboBox_textColor
            // 
            this.comboBox_textColor.FormattingEnabled = true;
            this.comboBox_textColor.Location = new System.Drawing.Point(234, 28);
            this.comboBox_textColor.Name = "comboBox_textColor";
            this.comboBox_textColor.Size = new System.Drawing.Size(110, 21);
            this.comboBox_textColor.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 389);
            this.Controls.Add(this.comboBox_textColor);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.but_generate);
            this.Controls.Add(this.comboBox_backgroundColor);
            this.Controls.Add(this.box_stepSize);
            this.Controls.Add(this.but_openFile);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button but_openFile;
        private System.Windows.Forms.TextBox box_stepSize;
        private System.Windows.Forms.ComboBox comboBox_backgroundColor;
        private System.Windows.Forms.Button but_generate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox_textColor;
    }
}