using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualizationForm
{
    partial class TagsCloudForm
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
            this.GenerateBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.colorLabel = new System.Windows.Forms.Label();
            this.colorTextBox = new System.Windows.Forms.TextBox();
            this.formPointGeneratorLabel = new System.Windows.Forms.Label();
            this.inputFilePathLabel = new System.Windows.Forms.Label();
            this.fontNameLabel = new System.Windows.Forms.Label();
            this.fontNameTextBox = new System.Windows.Forms.TextBox();
            this.formPointGeneratorComboBox = new System.Windows.Forms.ComboBox();
            this.inputFilePathBtn = new System.Windows.Forms.Button();
            this.imageSizeLabel = new System.Windows.Forms.Label();
            this.imageSizeTextBox1 = new System.Windows.Forms.TextBox();
            this.imageSizeTextBox2 = new System.Windows.Forms.TextBox();
            this.imageSizeDelimeterLabel = new System.Windows.Forms.Label();
            this.outFormatComboBox = new System.Windows.Forms.ComboBox();
            this.outFormatLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(54, 216);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(89, 22);
            this.GenerateBtn.TabIndex = 1;
            this.GenerateBtn.Text = "Вжух!";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(162, 216);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(89, 22);
            this.exitBtn.TabIndex = 2;
            this.exitBtn.Text = "Выйти";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(12, 27);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(32, 13);
            this.colorLabel.TabIndex = 3;
            this.colorLabel.Text = "Цвет";
            // 
            // colorTextBox
            // 
            this.colorTextBox.Location = new System.Drawing.Point(161, 27);
            this.colorTextBox.Name = "colorTextBox";
            this.colorTextBox.Size = new System.Drawing.Size(136, 20);
            this.colorTextBox.TabIndex = 0;
            this.colorTextBox.Text = "rainbow";
            // 
            // formPointGeneratorLabel
            // 
            this.formPointGeneratorLabel.AutoSize = true;
            this.formPointGeneratorLabel.Location = new System.Drawing.Point(12, 53);
            this.formPointGeneratorLabel.Name = "formPointGeneratorLabel";
            this.formPointGeneratorLabel.Size = new System.Drawing.Size(136, 13);
            this.formPointGeneratorLabel.TabIndex = 5;
            this.formPointGeneratorLabel.Text = "Форма генератора точек";
            // 
            // inputFilePathLabel
            // 
            this.inputFilePathLabel.AutoSize = true;
            this.inputFilePathLabel.Location = new System.Drawing.Point(12, 79);
            this.inputFilePathLabel.Name = "inputFilePathLabel";
            this.inputFilePathLabel.Size = new System.Drawing.Size(143, 13);
            this.inputFilePathLabel.TabIndex = 7;
            this.inputFilePathLabel.Text = "Путь до файла со словами";
            // 
            // fontNameLabel
            // 
            this.fontNameLabel.AutoSize = true;
            this.fontNameLabel.Location = new System.Drawing.Point(12, 105);
            this.fontNameLabel.Name = "fontNameLabel";
            this.fontNameLabel.Size = new System.Drawing.Size(99, 13);
            this.fontNameLabel.TabIndex = 9;
            this.fontNameLabel.Text = "Название шрифта";
            // 
            // fontNameTextBox
            // 
            this.fontNameTextBox.Location = new System.Drawing.Point(161, 105);
            this.fontNameTextBox.Name = "fontNameTextBox";
            this.fontNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.fontNameTextBox.TabIndex = 8;
            this.fontNameTextBox.Text = "Arial";
            // 
            // formPointGeneratorComboBox
            // 
            this.formPointGeneratorComboBox.FormattingEnabled = true;
            this.formPointGeneratorComboBox.Items.AddRange(new object[] {
            "heart",
            "spiral",
            "astroid"});
            this.formPointGeneratorComboBox.Location = new System.Drawing.Point(161, 53);
            this.formPointGeneratorComboBox.Name = "formPointGeneratorComboBox";
            this.formPointGeneratorComboBox.Size = new System.Drawing.Size(136, 21);
            this.formPointGeneratorComboBox.TabIndex = 10;
            this.formPointGeneratorComboBox.Text = "spiral";
            // 
            // inputFilePathBtn
            // 
            this.inputFilePathBtn.Location = new System.Drawing.Point(161, 79);
            this.inputFilePathBtn.Name = "inputFilePathBtn";
            this.inputFilePathBtn.Size = new System.Drawing.Size(137, 22);
            this.inputFilePathBtn.TabIndex = 11;
            this.inputFilePathBtn.Text = "Выбрать";
            this.inputFilePathBtn.UseVisualStyleBackColor = true;
            this.inputFilePathBtn.Click += new System.EventHandler(this.inputFilePathBtn_Click);
            // 
            // imageSizeLabel
            // 
            this.imageSizeLabel.AutoSize = true;
            this.imageSizeLabel.Location = new System.Drawing.Point(12, 131);
            this.imageSizeLabel.Name = "imageSizeLabel";
            this.imageSizeLabel.Size = new System.Drawing.Size(96, 13);
            this.imageSizeLabel.TabIndex = 13;
            this.imageSizeLabel.Text = "Размер картинки";
            // 
            // imageSizeTextBox1
            // 
            this.imageSizeTextBox1.Location = new System.Drawing.Point(161, 131);
            this.imageSizeTextBox1.Name = "imageSizeTextBox1";
            this.imageSizeTextBox1.Size = new System.Drawing.Size(56, 20);
            this.imageSizeTextBox1.TabIndex = 12;
            this.imageSizeTextBox1.Text = "800";
            // 
            // imageSizeTextBox2
            // 
            this.imageSizeTextBox2.Location = new System.Drawing.Point(242, 131);
            this.imageSizeTextBox2.Name = "imageSizeTextBox2";
            this.imageSizeTextBox2.Size = new System.Drawing.Size(56, 20);
            this.imageSizeTextBox2.TabIndex = 14;
            this.imageSizeTextBox2.Text = "600";
            // 
            // imageSizeDelimeterLabel
            // 
            this.imageSizeDelimeterLabel.AutoSize = true;
            this.imageSizeDelimeterLabel.Location = new System.Drawing.Point(223, 134);
            this.imageSizeDelimeterLabel.Name = "imageSizeDelimeterLabel";
            this.imageSizeDelimeterLabel.Size = new System.Drawing.Size(12, 13);
            this.imageSizeDelimeterLabel.TabIndex = 15;
            this.imageSizeDelimeterLabel.Text = "x";
            // 
            // outFormatComboBox
            // 
            this.outFormatComboBox.FormattingEnabled = true;
            this.outFormatComboBox.Items.AddRange(new object[] {
            "jpeg",
            "tiff",
            "png"});
            this.outFormatComboBox.Location = new System.Drawing.Point(162, 157);
            this.outFormatComboBox.Name = "outFormatComboBox";
            this.outFormatComboBox.Size = new System.Drawing.Size(136, 21);
            this.outFormatComboBox.TabIndex = 17;
            this.outFormatComboBox.Text = "jpeg";
            // 
            // outFormatLabel
            // 
            this.outFormatLabel.AutoSize = true;
            this.outFormatLabel.Location = new System.Drawing.Point(12, 160);
            this.outFormatLabel.Name = "outFormatLabel";
            this.outFormatLabel.Size = new System.Drawing.Size(120, 13);
            this.outFormatLabel.TabIndex = 16;
            this.outFormatLabel.Text = "Формат изображения";
            // 
            // TagsCloudForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 267);
            this.Controls.Add(this.outFormatComboBox);
            this.Controls.Add(this.outFormatLabel);
            this.Controls.Add(this.imageSizeDelimeterLabel);
            this.Controls.Add(this.imageSizeTextBox2);
            this.Controls.Add(this.imageSizeLabel);
            this.Controls.Add(this.imageSizeTextBox1);
            this.Controls.Add(this.inputFilePathBtn);
            this.Controls.Add(this.formPointGeneratorComboBox);
            this.Controls.Add(this.fontNameLabel);
            this.Controls.Add(this.fontNameTextBox);
            this.Controls.Add(this.inputFilePathLabel);
            this.Controls.Add(this.formPointGeneratorLabel);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.colorTextBox);
            this.Name = "TagsCloudForm";
            this.Text = "CloudTagsVisualization";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox colorTextBox;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Label formPointGeneratorLabel;
        private System.Windows.Forms.Label inputFilePathLabel;
        private System.Windows.Forms.Label fontNameLabel;
        private System.Windows.Forms.TextBox fontNameTextBox;
        private System.Windows.Forms.ComboBox formPointGeneratorComboBox;
        private System.Windows.Forms.Button inputFilePathBtn;
        private System.Windows.Forms.Label imageSizeLabel;
        private System.Windows.Forms.TextBox imageSizeTextBox1;
        private System.Windows.Forms.TextBox imageSizeTextBox2;
        private System.Windows.Forms.Label imageSizeDelimeterLabel;
        private System.Windows.Forms.ComboBox outFormatComboBox;
        private System.Windows.Forms.Label outFormatLabel;
    }
}

