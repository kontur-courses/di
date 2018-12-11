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
            this.formPointGeneratorTextBox = new System.Windows.Forms.TextBox();
            this.inputFilePathLabel = new System.Windows.Forms.Label();
            this.inputFilePathTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(570, 402);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(89, 22);
            this.GenerateBtn.TabIndex = 1;
            this.GenerateBtn.Text = "Вжух!";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(678, 402);
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
            this.colorTextBox.Size = new System.Drawing.Size(137, 20);
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
            // formPointGeneratorTextBox
            // 
            this.formPointGeneratorTextBox.Location = new System.Drawing.Point(161, 53);
            this.formPointGeneratorTextBox.Name = "formPointGeneratorTextBox";
            this.formPointGeneratorTextBox.Size = new System.Drawing.Size(137, 20);
            this.formPointGeneratorTextBox.TabIndex = 4;
            this.formPointGeneratorTextBox.Text = "spiral";
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
            // inputFilePathTextBox
            // 
            this.inputFilePathTextBox.Location = new System.Drawing.Point(161, 79);
            this.inputFilePathTextBox.Name = "inputFilePathTextBox";
            this.inputFilePathTextBox.Size = new System.Drawing.Size(137, 20);
            this.inputFilePathTextBox.TabIndex = 6;
            this.inputFilePathTextBox.Text = "input.txt";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "label4";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(161, 105);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(137, 20);
            this.textBox4.TabIndex = 8;
            // 
            // TagsCloudForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.inputFilePathLabel);
            this.Controls.Add(this.inputFilePathTextBox);
            this.Controls.Add(this.formPointGeneratorLabel);
            this.Controls.Add(this.formPointGeneratorTextBox);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.colorTextBox);
            this.Name = "TagsCloudForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox colorTextBox;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Label formPointGeneratorLabel;
        private System.Windows.Forms.TextBox formPointGeneratorTextBox;
        private System.Windows.Forms.Label inputFilePathLabel;
        private System.Windows.Forms.TextBox inputFilePathTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
    }
}

