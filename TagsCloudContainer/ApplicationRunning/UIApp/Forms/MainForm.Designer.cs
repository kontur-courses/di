using System.ComponentModel;

namespace TagsCloudContainer.ApplicationRunning.UIApp.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ChooseTextFileButton = new System.Windows.Forms.Button();
            this.LayoutingAlgorithmComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LayouterSquareMultiplier = new System.Windows.Forms.NumericUpDown();
            this.LayouterStep = new System.Windows.Forms.NumericUpDown();
            this.LayouterBroadness = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.WidthNumeric = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.HeightNumeric = new System.Windows.Forms.NumericUpDown();
            this.BackgroundColorDialog = new System.Windows.Forms.ColorDialog();
            this.FirstColorDialog = new System.Windows.Forms.ColorDialog();
            this.SecondColorDialog = new System.Windows.Forms.ColorDialog();
            this.BackgroundColorButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.FirstColorButton = new System.Windows.Forms.Button();
            this.SecondColorButton = new System.Windows.Forms.Button();
            this.GradientCheckBox = new System.Windows.Forms.CheckBox();
            this.ApplyVisualizationButton = new System.Windows.Forms.Button();
            this.FontButton = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.SaveImageButton = new System.Windows.Forms.Button();
            this.SaveImageDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize) (this.PreviewPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.LayouterSquareMultiplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.LayouterStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.LayouterBroadness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.WidthNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.HeightNumeric)).BeginInit();
            this.SuspendLayout();
            this.PreviewPictureBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.PreviewPictureBox.Location = new System.Drawing.Point(22, 22);
            this.PreviewPictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.PreviewPictureBox.Size = new System.Drawing.Size(520, 520);
            this.PreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PreviewPictureBox.TabIndex = 0;
            this.PreviewPictureBox.TabStop = false;
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Text files (*.txt)|*.txt|Word documents (*.doc; *.docx)|*.doc; *.docx";
            this.openFileDialog1.Title = "Browse Text Files";
            this.ChooseTextFileButton.Location = new System.Drawing.Point(548, 22);
            this.ChooseTextFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChooseTextFileButton.Name = "ChooseTextFileButton";
            this.ChooseTextFileButton.Size = new System.Drawing.Size(134, 41);
            this.ChooseTextFileButton.TabIndex = 1;
            this.ChooseTextFileButton.Text = "Choose text file";
            this.ChooseTextFileButton.UseVisualStyleBackColor = true;
            this.ChooseTextFileButton.Click += new System.EventHandler(this.ChooseTextFileButton_Click);
            this.LayoutingAlgorithmComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.LayoutingAlgorithmComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.LayoutingAlgorithmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayoutingAlgorithmComboBox.FormattingEnabled = true;
            this.LayoutingAlgorithmComboBox.Items.AddRange(new object[] {"Circular"});
            this.LayoutingAlgorithmComboBox.Location = new System.Drawing.Point(548, 100);
            this.LayoutingAlgorithmComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LayoutingAlgorithmComboBox.Name = "LayoutingAlgorithmComboBox";
            this.LayoutingAlgorithmComboBox.Size = new System.Drawing.Size(178, 28);
            this.LayoutingAlgorithmComboBox.TabIndex = 2;
            this.LayoutingAlgorithmComboBox.SelectedIndex = 0;
            this.label1.Location = new System.Drawing.Point(548, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Generate cloud";
            this.LayouterSquareMultiplier.Location = new System.Drawing.Point(548, 132);
            this.LayouterSquareMultiplier.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LayouterSquareMultiplier.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.LayouterSquareMultiplier.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.LayouterSquareMultiplier.Name = "LayouterSquareMultiplier";
            this.LayouterSquareMultiplier.Size = new System.Drawing.Size(91, 27);
            this.LayouterSquareMultiplier.TabIndex = 4;
            this.LayouterSquareMultiplier.Value = new decimal(new int[] {100, 0, 0, 0});
            this.LayouterStep.DecimalPlaces = 1;
            this.LayouterStep.Increment = new decimal(new int[] {1, 0, 0, 65536});
            this.LayouterStep.Location = new System.Drawing.Point(548, 165);
            this.LayouterStep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LayouterStep.Minimum = new decimal(new int[] {1, 0, 0, 65536});
            this.LayouterStep.Name = "LayouterStep";
            this.LayouterStep.Size = new System.Drawing.Size(91, 27);
            this.LayouterStep.TabIndex = 5;
            this.LayouterStep.Value = new decimal(new int[] {2, 0, 0, 65536});
            this.LayouterBroadness.Location = new System.Drawing.Point(548, 199);
            this.LayouterBroadness.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LayouterBroadness.Maximum = new decimal(new int[] {2, 0, 0, 0});
            this.LayouterBroadness.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.LayouterBroadness.Name = "LayouterBroadness";
            this.LayouterBroadness.Size = new System.Drawing.Size(91, 27);
            this.LayouterBroadness.TabIndex = 6;
            this.LayouterBroadness.Value = new decimal(new int[] {1, 0, 0, 0});
            this.label2.Location = new System.Drawing.Point(645, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = "Size";
            this.label3.Location = new System.Drawing.Point(645, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "Step";
            this.label4.Location = new System.Drawing.Point(645, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 22);
            this.label4.TabIndex = 9;
            this.label4.Text = "Broadness";
            this.GenerateButton.Enabled = false;
            this.GenerateButton.Location = new System.Drawing.Point(548, 231);
            this.GenerateButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(91, 28);
            this.GenerateButton.TabIndex = 10;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            this.label5.Location = new System.Drawing.Point(548, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "Visualize";
            this.label6.Location = new System.Drawing.Point(645, 299);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 22);
            this.label6.TabIndex = 14;
            this.label6.Text = "Width";
            this.WidthNumeric.Location = new System.Drawing.Point(548, 298);
            this.WidthNumeric.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.WidthNumeric.Maximum = new decimal(new int[] {8000, 0, 0, 0});
            this.WidthNumeric.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.WidthNumeric.Name = "WidthNumeric";
            this.WidthNumeric.Size = new System.Drawing.Size(91, 27);
            this.WidthNumeric.TabIndex = 13;
            this.WidthNumeric.Value = new decimal(new int[] {700, 0, 0, 0});
            this.label7.Location = new System.Drawing.Point(645, 330);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 22);
            this.label7.TabIndex = 16;
            this.label7.Text = "Height";
            this.HeightNumeric.Location = new System.Drawing.Point(548, 328);
            this.HeightNumeric.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HeightNumeric.Maximum = new decimal(new int[] {8000, 0, 0, 0});
            this.HeightNumeric.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.HeightNumeric.Name = "HeightNumeric";
            this.HeightNumeric.Size = new System.Drawing.Size(91, 27);
            this.HeightNumeric.TabIndex = 15;
            this.HeightNumeric.Value = new decimal(new int[] {700, 0, 0, 0});
            this.FirstColorDialog.Color = System.Drawing.Color.Red;
            this.SecondColorDialog.Color = System.Drawing.Color.SkyBlue;
            this.BackgroundColorButton.Location = new System.Drawing.Point(548, 382);
            this.BackgroundColorButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BackgroundColorButton.Name = "BackgroundColorButton";
            this.BackgroundColorButton.Size = new System.Drawing.Size(115, 29);
            this.BackgroundColorButton.TabIndex = 17;
            this.BackgroundColorButton.Text = "Background";
            this.BackgroundColorButton.UseVisualStyleBackColor = true;
            this.BackgroundColorButton.Click += new System.EventHandler(this.BackgroundColorButton_Click);
            this.label8.Location = new System.Drawing.Point(548, 358);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 18);
            this.label8.TabIndex = 18;
            this.label8.Text = "Colors";
            this.FirstColorButton.Location = new System.Drawing.Point(669, 382);
            this.FirstColorButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FirstColorButton.Name = "FirstColorButton";
            this.FirstColorButton.Size = new System.Drawing.Size(115, 29);
            this.FirstColorButton.TabIndex = 19;
            this.FirstColorButton.Text = "First";
            this.FirstColorButton.UseVisualStyleBackColor = true;
            this.FirstColorButton.Click += new System.EventHandler(this.FirstColorButton_Click);
            this.SecondColorButton.Location = new System.Drawing.Point(790, 382);
            this.SecondColorButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SecondColorButton.Name = "SecondColorButton";
            this.SecondColorButton.Size = new System.Drawing.Size(115, 29);
            this.SecondColorButton.TabIndex = 20;
            this.SecondColorButton.Text = "Second";
            this.SecondColorButton.UseVisualStyleBackColor = true;
            this.SecondColorButton.Click += new System.EventHandler(this.SecondColorButton_Click);
            this.GradientCheckBox.Location = new System.Drawing.Point(548, 418);
            this.GradientCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GradientCheckBox.Name = "GradientCheckBox";
            this.GradientCheckBox.Size = new System.Drawing.Size(115, 29);
            this.GradientCheckBox.TabIndex = 21;
            this.GradientCheckBox.Text = "Gradient";
            this.GradientCheckBox.UseVisualStyleBackColor = true;
            this.ApplyVisualizationButton.Enabled = false;
            this.ApplyVisualizationButton.Location = new System.Drawing.Point(548, 451);
            this.ApplyVisualizationButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ApplyVisualizationButton.Name = "ApplyVisualizationButton";
            this.ApplyVisualizationButton.Size = new System.Drawing.Size(115, 29);
            this.ApplyVisualizationButton.TabIndex = 22;
            this.ApplyVisualizationButton.Text = "Apply";
            this.ApplyVisualizationButton.UseVisualStyleBackColor = true;
            this.ApplyVisualizationButton.Click += new System.EventHandler(this.ApplyVisualizationButton_Click);
            this.FontButton.Location = new System.Drawing.Point(790, 326);
            this.FontButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FontButton.Name = "FontButton";
            this.FontButton.Size = new System.Drawing.Size(114, 29);
            this.FontButton.TabIndex = 23;
            this.FontButton.Text = "Font";
            this.FontButton.UseVisualStyleBackColor = true;
            this.FontButton.Click += new System.EventHandler(this.FontButton_Click);
            this.fontDialog1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.fontDialog1.MaxSize = 16;
            this.fontDialog1.MinSize = 16;
            this.SaveImageButton.Enabled = false;
            this.SaveImageButton.Location = new System.Drawing.Point(548, 512);
            this.SaveImageButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveImageButton.Name = "SaveImageButton";
            this.SaveImageButton.Size = new System.Drawing.Size(115, 29);
            this.SaveImageButton.TabIndex = 24;
            this.SaveImageButton.Text = "Save Image";
            this.SaveImageButton.UseVisualStyleBackColor = true;
            this.SaveImageButton.Click += new System.EventHandler(this.SaveImageButton_Click);
            this.SaveImageDialog.Filter = "Bmp (*.bmp)|*.bmp|Jpeg (*.jpg)|*.jpg|PNG (*.png)|*.png";
            this.SaveImageDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveImageDialog_FileOk);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(940, 565);
            this.Controls.Add(this.SaveImageButton);
            this.Controls.Add(this.FontButton);
            this.Controls.Add(this.ApplyVisualizationButton);
            this.Controls.Add(this.GradientCheckBox);
            this.Controls.Add(this.SecondColorButton);
            this.Controls.Add(this.FirstColorButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.BackgroundColorButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.HeightNumeric);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.WidthNumeric);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LayouterBroadness);
            this.Controls.Add(this.LayouterStep);
            this.Controls.Add(this.LayouterSquareMultiplier);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LayoutingAlgorithmComboBox);
            this.Controls.Add(this.ChooseTextFileButton);
            this.Controls.Add(this.PreviewPictureBox);
            this.Location = new System.Drawing.Point(19, 19);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(958, 612);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(958, 612);
            this.Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize) (this.PreviewPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.LayouterSquareMultiplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.LayouterStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.LayouterBroadness)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.WidthNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.HeightNumeric)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ChooseTextFileButton;
        private System.Windows.Forms.ComboBox LayoutingAlgorithmComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown LayouterSquareMultiplier;
        private System.Windows.Forms.NumericUpDown LayouterBroadness;
        private System.Windows.Forms.NumericUpDown LayouterStep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColorDialog SecondColorDialog;
        private System.Windows.Forms.ColorDialog FirstColorDialog;
        private System.Windows.Forms.ColorDialog BackgroundColorDialog;
        private System.Windows.Forms.CheckBox GradientCheckBox;
        private System.Windows.Forms.Button SecondColorButton;
        private System.Windows.Forms.Button FirstColorButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button BackgroundColorButton;
        private System.Windows.Forms.Button ApplyVisualizationButton;
        private System.Windows.Forms.NumericUpDown WidthNumeric;
        private System.Windows.Forms.NumericUpDown HeightNumeric;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button FontButton;
        private System.Windows.Forms.SaveFileDialog SaveImageDialog;
        private System.Windows.Forms.Button SaveImageButton;
        private System.Windows.Forms.PictureBox PreviewPictureBox;
    }
}