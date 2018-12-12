namespace TagCloudContainer.Gui
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                resultImage.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.resultPictureBox = new System.Windows.Forms.PictureBox();
            this.wordsTextBox = new System.Windows.Forms.TextBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.generateButton = new System.Windows.Forms.Button();
            this.saveResultButton = new System.Windows.Forms.Button();
            this.chooseFontColorButton = new System.Windows.Forms.Button();
            this.resultWidthTextBox = new System.Windows.Forms.TextBox();
            this.resultHeightTextBox = new System.Windows.Forms.TextBox();
            this.resultSizeXLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // resultPictureBox
            // 
            this.resultPictureBox.Location = new System.Drawing.Point(299, 12);
            this.resultPictureBox.Name = "resultPictureBox";
            this.resultPictureBox.Size = new System.Drawing.Size(489, 374);
            this.resultPictureBox.TabIndex = 0;
            this.resultPictureBox.TabStop = false;
            // 
            // wordsTextBox
            // 
            this.wordsTextBox.Location = new System.Drawing.Point(12, 12);
            this.wordsTextBox.Multiline = true;
            this.wordsTextBox.Name = "wordsTextBox";
            this.wordsTextBox.Size = new System.Drawing.Size(281, 374);
            this.wordsTextBox.TabIndex = 1;
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(13, 409);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(75, 23);
            this.openFileButton.TabIndex = 2;
            this.openFileButton.Text = "Открыть файл";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(94, 409);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(101, 23);
            this.generateButton.TabIndex = 3;
            this.generateButton.Text = "Сгенерировать";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // saveResultButton
            // 
            this.saveResultButton.Location = new System.Drawing.Point(201, 409);
            this.saveResultButton.Name = "saveResultButton";
            this.saveResultButton.Size = new System.Drawing.Size(75, 23);
            this.saveResultButton.TabIndex = 4;
            this.saveResultButton.Text = "Сохранить";
            this.saveResultButton.UseVisualStyleBackColor = true;
            this.saveResultButton.Click += new System.EventHandler(this.SaveResultButton_Click);
            // 
            // chooseFontColorButton
            // 
            this.chooseFontColorButton.Location = new System.Drawing.Point(697, 409);
            this.chooseFontColorButton.Name = "chooseFontColorButton";
            this.chooseFontColorButton.Size = new System.Drawing.Size(91, 23);
            this.chooseFontColorButton.TabIndex = 5;
            this.chooseFontColorButton.Text = "Цвет шрифта";
            this.chooseFontColorButton.UseVisualStyleBackColor = true;
            this.chooseFontColorButton.Click += new System.EventHandler(this.ChooseFontColor_Click);
            // 
            // resultWidthTextBox
            // 
            this.resultWidthTextBox.Location = new System.Drawing.Point(557, 411);
            this.resultWidthTextBox.Name = "resultWidthTextBox";
            this.resultWidthTextBox.Size = new System.Drawing.Size(47, 20);
            this.resultWidthTextBox.TabIndex = 6;
            this.resultWidthTextBox.Text = "600";
            // 
            // resultHeightTextBox
            // 
            this.resultHeightTextBox.Location = new System.Drawing.Point(630, 409);
            this.resultHeightTextBox.Name = "resultHeightTextBox";
            this.resultHeightTextBox.Size = new System.Drawing.Size(50, 20);
            this.resultHeightTextBox.TabIndex = 7;
            this.resultHeightTextBox.Text = "600";
            // 
            // resultSizeXLabel
            // 
            this.resultSizeXLabel.AutoSize = true;
            this.resultSizeXLabel.Location = new System.Drawing.Point(610, 414);
            this.resultSizeXLabel.Name = "resultSizeXLabel";
            this.resultSizeXLabel.Size = new System.Drawing.Size(14, 13);
            this.resultSizeXLabel.TabIndex = 8;
            this.resultSizeXLabel.Text = "X";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.resultSizeXLabel);
            this.Controls.Add(this.resultHeightTextBox);
            this.Controls.Add(this.resultWidthTextBox);
            this.Controls.Add(this.chooseFontColorButton);
            this.Controls.Add(this.saveResultButton);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.wordsTextBox);
            this.Controls.Add(this.resultPictureBox);
            this.Name = "MainForm";
            this.Text = "Облако слов";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox resultPictureBox;
        private System.Windows.Forms.TextBox wordsTextBox;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Button saveResultButton;
        private System.Windows.Forms.Button chooseFontColorButton;
        private System.Windows.Forms.TextBox resultWidthTextBox;
        private System.Windows.Forms.TextBox resultHeightTextBox;
        private System.Windows.Forms.Label resultSizeXLabel;
    }
}

