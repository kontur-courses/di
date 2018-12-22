using Autofac;
using Autofac.Core.Lifetime;
using WordCloud.Properties;

namespace WordCloud
{
    partial class TagClodForm
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
            this.GoBtn = new System.Windows.Forms.Button();
            this.analyzedText = new System.Windows.Forms.RichTextBox();
            this.savedImgTxt = new System.Windows.Forms.TextBox();
            this.saveImgLabel = new System.Windows.Forms.Label();
            this.browseBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openTextFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.maxFont = new System.Windows.Forms.NumericUpDown();
            this.minFont = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.orthogonalLayoutRadioButton = new System.Windows.Forms.RadioButton();
            this.spiralLayoutRadioButton = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.maxFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // GoBtn
            // 
            this.GoBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GoBtn.Location = new System.Drawing.Point(627, 288);
            this.GoBtn.Name = "GoBtn";
            this.GoBtn.Size = new System.Drawing.Size(323, 23);
            this.GoBtn.TabIndex = 1;
            this.GoBtn.Text = "GO";
            this.GoBtn.UseVisualStyleBackColor = true;
            this.GoBtn.Click += new System.EventHandler(this.GoBtn_Click);
            // 
            // analyzedText
            // 
            this.analyzedText.Location = new System.Drawing.Point(627, 29);
            this.analyzedText.Name = "analyzedText";
            this.analyzedText.Size = new System.Drawing.Size(323, 116);
            this.analyzedText.TabIndex = 3;
            this.analyzedText.Text = "";
            // 
            // savedImgTxt
            // 
            this.savedImgTxt.Enabled = false;
            this.savedImgTxt.Location = new System.Drawing.Point(12, 462);
            this.savedImgTxt.Name = "savedImgTxt";
            this.savedImgTxt.Size = new System.Drawing.Size(609, 20);
            this.savedImgTxt.TabIndex = 5;
            // 
            // saveImgLabel
            // 
            this.saveImgLabel.AutoSize = true;
            this.saveImgLabel.Location = new System.Drawing.Point(12, 434);
            this.saveImgLabel.Name = "saveImgLabel";
            this.saveImgLabel.Size = new System.Drawing.Size(136, 13);
            this.saveImgLabel.TabIndex = 6;
            this.saveImgLabel.Text = "Изображение сохранено:";
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(627, 151);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(323, 23);
            this.browseBtn.TabIndex = 7;
            this.browseBtn.Text = "Выбрать из файла";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(627, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Введите текст или выберите из файла";
            // 
            // openTextFileDialog
            // 
            this.openTextFileDialog.FileName = "Выберите файл";
            this.openTextFileDialog.Filter = global::WordCloud.Properties.Resources.TxtFilter;
            // 
            // maxFont
            // 
            this.maxFont.Location = new System.Drawing.Point(627, 205);
            this.maxFont.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.maxFont.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.maxFont.Name = "maxFont";
            this.maxFont.Size = new System.Drawing.Size(120, 20);
            this.maxFont.TabIndex = 9;
            this.maxFont.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // minFont
            // 
            this.minFont.Location = new System.Drawing.Point(831, 205);
            this.minFont.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.minFont.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.minFont.Name = "minFont";
            this.minFont.Size = new System.Drawing.Size(120, 20);
            this.minFont.TabIndex = 10;
            this.minFont.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(828, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Минимальный шрифт";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(624, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Максимальный шрифт";
            // 
            // orthogonalLayoutRadioButton
            // 
            this.orthogonalLayoutRadioButton.AutoSize = true;
            this.orthogonalLayoutRadioButton.Location = new System.Drawing.Point(630, 248);
            this.orthogonalLayoutRadioButton.Name = "orthogonalLayoutRadioButton";
            this.orthogonalLayoutRadioButton.Size = new System.Drawing.Size(108, 17);
            this.orthogonalLayoutRadioButton.TabIndex = 14;
            this.orthogonalLayoutRadioButton.Text = "Orthogonal layout";
            this.orthogonalLayoutRadioButton.UseVisualStyleBackColor = true;
            // 
            // spiralLayoutRadioButton
            // 
            this.spiralLayoutRadioButton.AutoSize = true;
            this.spiralLayoutRadioButton.Checked = true;
            this.spiralLayoutRadioButton.Location = new System.Drawing.Point(831, 248);
            this.spiralLayoutRadioButton.Name = "spiralLayoutRadioButton";
            this.spiralLayoutRadioButton.Size = new System.Drawing.Size(86, 17);
            this.spiralLayoutRadioButton.TabIndex = 15;
            this.spiralLayoutRadioButton.TabStop = true;
            this.spiralLayoutRadioButton.Text = "Spiral Layout";
            this.spiralLayoutRadioButton.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(606, 403);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // TagClodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 494);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.spiralLayoutRadioButton);
            this.Controls.Add(this.orthogonalLayoutRadioButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.minFont);
            this.Controls.Add(this.maxFont);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.saveImgLabel);
            this.Controls.Add(this.savedImgTxt);
            this.Controls.Add(this.analyzedText);
            this.Controls.Add(this.GoBtn);
            this.Name = "TagClodForm";
            this.Text = "Word cloud";
            ((System.ComponentModel.ISupportInitialize)(this.maxFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button GoBtn;
        private System.Windows.Forms.RichTextBox analyzedText;
        private System.Windows.Forms.TextBox savedImgTxt;
        private System.Windows.Forms.Label saveImgLabel;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openTextFileDialog;
        private System.Windows.Forms.NumericUpDown maxFont;
        private System.Windows.Forms.NumericUpDown minFont;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton orthogonalLayoutRadioButton;
        private System.Windows.Forms.RadioButton spiralLayoutRadioButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

