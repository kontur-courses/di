using System.Drawing;

namespace TagCloudGUIClient
{
    partial class Form1
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.layouterSelector = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.fileSelector = new System.Windows.Forms.Button();
            this.fontSelector = new System.Windows.Forms.ComboBox();
            this.sizeSelector = new System.Windows.Forms.ComboBox();
            this.colorSelectorSelector = new System.Windows.Forms.ComboBox();
            this.drawButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(768, 593);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.69398F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.30602F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 599F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1111, 599);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AllowDrop = true;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.layouterSelector);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.fontSelector);
            this.flowLayoutPanel1.Controls.Add(this.sizeSelector);
            this.flowLayoutPanel1.Controls.Add(this.colorSelectorSelector);
            this.flowLayoutPanel1.Controls.Add(this.drawButton);
            this.flowLayoutPanel1.Controls.Add(this.saveButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(777, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(331, 593);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // layouterSelector
            // 
            this.layouterSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layouterSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layouterSelector.Location = new System.Drawing.Point(3, 3);
            this.layouterSelector.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.layouterSelector.Name = "layouterSelector";
            this.layouterSelector.Size = new System.Drawing.Size(316, 23);
            this.layouterSelector.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.textBox1);
            this.flowLayoutPanel2.Controls.Add(this.fileSelector);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 49);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(316, 41);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 23);
            this.textBox1.TabIndex = 1;
            // 
            // fileSelector
            // 
            this.fileSelector.Location = new System.Drawing.Point(212, 3);
            this.fileSelector.Name = "fileSelector";
            this.fileSelector.Size = new System.Drawing.Size(74, 23);
            this.fileSelector.TabIndex = 2;
            this.fileSelector.Text = "Chose file";
            this.fileSelector.UseVisualStyleBackColor = true;
            this.fileSelector.Click += new System.EventHandler(this.fileSelector_Click);
            // 
            // fontSelector
            // 
            this.fontSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fontSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontSelector.FormattingEnabled = true;
            this.fontSelector.Location = new System.Drawing.Point(3, 113);
            this.fontSelector.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.fontSelector.Name = "fontSelector";
            this.fontSelector.Size = new System.Drawing.Size(316, 23);
            this.fontSelector.TabIndex = 4;
            // 
            // sizeSelector
            // 
            this.sizeSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sizeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sizeSelector.FormattingEnabled = true;
            this.sizeSelector.Items.AddRange(new object[] {
            "3840x2160",
            "1920x1080",
            "1280x720",
            "800x600"});
            this.sizeSelector.Location = new System.Drawing.Point(3, 159);
            this.sizeSelector.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.sizeSelector.Name = "sizeSelector";
            this.sizeSelector.Size = new System.Drawing.Size(316, 23);
            this.sizeSelector.TabIndex = 5;
            this.sizeSelector.SelectedIndexChanged += new System.EventHandler(this.sizeSelector_SelectedIndexChanged);
            // 
            // colorSelectorSelector
            // 
            this.colorSelectorSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorSelectorSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorSelectorSelector.FormattingEnabled = true;
            this.colorSelectorSelector.Location = new System.Drawing.Point(3, 205);
            this.colorSelectorSelector.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.colorSelectorSelector.Name = "colorSelectorSelector";
            this.colorSelectorSelector.Size = new System.Drawing.Size(316, 23);
            this.colorSelectorSelector.TabIndex = 6;
            // 
            // drawButton
            // 
            this.drawButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawButton.Location = new System.Drawing.Point(3, 251);
            this.drawButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(316, 78);
            this.drawButton.TabIndex = 7;
            this.drawButton.Text = "Draw";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.saveButton.Location = new System.Drawing.Point(3, 352);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(316, 84);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 599);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox layouterSelector;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button fileSelector;
        private System.Windows.Forms.ComboBox fontSelector;
        private System.Windows.Forms.ComboBox sizeSelector;
        private System.Windows.Forms.ComboBox colorSelectorSelector;
        private System.Windows.Forms.Button drawButton;
        private System.Windows.Forms.Button saveButton;
    }
}