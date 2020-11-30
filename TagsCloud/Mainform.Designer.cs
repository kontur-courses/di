namespace TagsCloud
{
    partial class Mainform
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.SetExcludedWordsButton = new System.Windows.Forms.Button();
            this.SetFontButton = new System.Windows.Forms.Button();
            this.ImageSaveButton = new System.Windows.Forms.Button();
            this.TextOpenButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AlgorithmChoice = new System.Windows.Forms.ComboBox();
            this.SetImageSizeButton = new System.Windows.Forms.Button();
            this.SetPaletteButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PictureBox);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.SetExcludedWordsButton);
            this.splitContainer1.Panel2.Controls.Add(this.SetFontButton);
            this.splitContainer1.Panel2.Controls.Add(this.ImageSaveButton);
            this.splitContainer1.Panel2.Controls.Add(this.TextOpenButton);
            this.splitContainer1.Panel2.Controls.Add(this.StartButton);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.AlgorithmChoice);
            this.splitContainer1.Panel2.Controls.Add(this.SetImageSizeButton);
            this.splitContainer1.Panel2.Controls.Add(this.SetPaletteButton);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 650;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // PictureBox
            // 
            this.PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox.Location = new System.Drawing.Point(0, 0);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(646, 446);
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            // 
            // SetExcludedWordsButton
            // 
            this.SetExcludedWordsButton.Location = new System.Drawing.Point(0, 357);
            this.SetExcludedWordsButton.Name = "SetExcludedWordsButton";
            this.SetExcludedWordsButton.Size = new System.Drawing.Size(144, 23);
            this.SetExcludedWordsButton.TabIndex = 8;
            this.SetExcludedWordsButton.Text = "Задать исключения";
            this.SetExcludedWordsButton.UseVisualStyleBackColor = true;
            this.SetExcludedWordsButton.Click += new System.EventHandler(this.SetExcludedWordsButton_Click);
            // 
            // SetFontButton
            // 
            this.SetFontButton.Location = new System.Drawing.Point(0, 59);
            this.SetFontButton.Name = "SetFontButton";
            this.SetFontButton.Size = new System.Drawing.Size(144, 23);
            this.SetFontButton.TabIndex = 7;
            this.SetFontButton.Text = "Настроить шрифт";
            this.SetFontButton.UseVisualStyleBackColor = true;
            this.SetFontButton.Click += new System.EventHandler(this.SetFontButton_Click);
            // 
            // ImageSaveButton
            // 
            this.ImageSaveButton.Location = new System.Drawing.Point(0, 293);
            this.ImageSaveButton.Name = "ImageSaveButton";
            this.ImageSaveButton.Size = new System.Drawing.Size(144, 23);
            this.ImageSaveButton.TabIndex = 6;
            this.ImageSaveButton.Text = "Сохранить в файл";
            this.ImageSaveButton.UseVisualStyleBackColor = true;
            this.ImageSaveButton.Click += new System.EventHandler(this.ImageSaveButton_Click);
            // 
            // TextOpenButton
            // 
            this.TextOpenButton.Location = new System.Drawing.Point(0, 264);
            this.TextOpenButton.Name = "TextOpenButton";
            this.TextOpenButton.Size = new System.Drawing.Size(144, 23);
            this.TextOpenButton.TabIndex = 5;
            this.TextOpenButton.Text = "Задать текст";
            this.TextOpenButton.UseVisualStyleBackColor = true;
            this.TextOpenButton.Click += new System.EventHandler(this.TextOpenButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(0, 190);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(144, 58);
            this.StartButton.TabIndex = 4;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Алгоритм";
            // 
            // AlgorithmChoice
            // 
            this.AlgorithmChoice.FormattingEnabled = true;
            this.AlgorithmChoice.Items.AddRange(new object[] {
            "asd",
            "dfh",
            "fgh",
            "jh"});
            this.AlgorithmChoice.Location = new System.Drawing.Point(0, 127);
            this.AlgorithmChoice.Name = "AlgorithmChoice";
            this.AlgorithmChoice.Size = new System.Drawing.Size(142, 23);
            this.AlgorithmChoice.TabIndex = 2;
            // 
            // SetImageSizeButton
            // 
            this.SetImageSizeButton.Location = new System.Drawing.Point(0, 29);
            this.SetImageSizeButton.Name = "SetImageSizeButton";
            this.SetImageSizeButton.Size = new System.Drawing.Size(144, 23);
            this.SetImageSizeButton.TabIndex = 1;
            this.SetImageSizeButton.Text = "Настроить размер";
            this.SetImageSizeButton.UseVisualStyleBackColor = true;
            this.SetImageSizeButton.Click += new System.EventHandler(this.SetImageSizeButton_Click);
            // 
            // SetPaletteButton
            // 
            this.SetPaletteButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.SetPaletteButton.Location = new System.Drawing.Point(0, 0);
            this.SetPaletteButton.Name = "SetPaletteButton";
            this.SetPaletteButton.Size = new System.Drawing.Size(142, 23);
            this.SetPaletteButton.TabIndex = 0;
            this.SetPaletteButton.Text = "Настроить палитру";
            this.SetPaletteButton.UseVisualStyleBackColor = true;
            this.SetPaletteButton.Click += new System.EventHandler(this.SetPaletteButton_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Mainform";
            this.Text = "Mainform";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button SetPaletteButton;
        private System.Windows.Forms.Button SetImageSizeButton;
        private System.Windows.Forms.ComboBox AlgorithmChoice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Button ImageSaveButton;
        private System.Windows.Forms.Button TextOpenButton;
        private System.Windows.Forms.Button SetFontButton;
        private System.Windows.Forms.Button SetExcludedWordsButton;
        private System.Windows.Forms.PictureBox PictureBox;
    }
}