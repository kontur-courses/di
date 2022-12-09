namespace TagsCloudWinformsApp;

partial class MainForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.fontButton = new System.Windows.Forms.Button();
            this.fontColor_label = new System.Windows.Forms.Label();
            this.fontColor_button = new System.Windows.Forms.Button();
            this.backgroundColor_label = new System.Windows.Forms.Label();
            this.backgroundColor_button = new System.Windows.Forms.Button();
            this.layout_label = new System.Windows.Forms.Label();
            this.layout_comboBox = new System.Windows.Forms.ComboBox();
            this.growthPercent_label = new System.Windows.Forms.Label();
            this.growthPercent_numeric = new System.Windows.Forms.NumericUpDown();
            this.imageWidth_label = new System.Windows.Forms.Label();
            this.imageWidth_numeric = new System.Windows.Forms.NumericUpDown();
            this.imageHeight_label = new System.Windows.Forms.Label();
            this.imageHeight_numeric = new System.Windows.Forms.NumericUpDown();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.generate_button = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.inputFile_button = new System.Windows.Forms.Button();
            this.chooseFilterFile_button = new System.Windows.Forms.Button();
            this.removeFilter_button = new System.Windows.Forms.Button();
            this.saveImage_button = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.growthPercent_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageWidth_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageHeight_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel1.Controls.Add(this.fontButton);
            this.flowLayoutPanel1.Controls.Add(this.fontColor_label);
            this.flowLayoutPanel1.Controls.Add(this.fontColor_button);
            this.flowLayoutPanel1.Controls.Add(this.backgroundColor_label);
            this.flowLayoutPanel1.Controls.Add(this.backgroundColor_button);
            this.flowLayoutPanel1.Controls.Add(this.layout_label);
            this.flowLayoutPanel1.Controls.Add(this.layout_comboBox);
            this.flowLayoutPanel1.Controls.Add(this.growthPercent_label);
            this.flowLayoutPanel1.Controls.Add(this.growthPercent_numeric);
            this.flowLayoutPanel1.Controls.Add(this.imageWidth_label);
            this.flowLayoutPanel1.Controls.Add(this.imageWidth_numeric);
            this.flowLayoutPanel1.Controls.Add(this.imageHeight_label);
            this.flowLayoutPanel1.Controls.Add(this.imageHeight_numeric);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(134, 446);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // fontButton
            // 
            this.fontButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fontButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fontButton.Location = new System.Drawing.Point(2, 2);
            this.fontButton.Margin = new System.Windows.Forms.Padding(0);
            this.fontButton.Name = "fontButton";
            this.fontButton.Size = new System.Drawing.Size(131, 55);
            this.fontButton.TabIndex = 1;
            this.fontButton.Text = "text\r\nbutton";
            this.fontButton.UseVisualStyleBackColor = true;
            this.fontButton.Click += new System.EventHandler(this.fontButton_Click);
            // 
            // fontColor_label
            // 
            this.fontColor_label.AutoSize = true;
            this.fontColor_label.Location = new System.Drawing.Point(2, 63);
            this.fontColor_label.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.fontColor_label.Name = "fontColor_label";
            this.fontColor_label.Size = new System.Drawing.Size(81, 20);
            this.fontColor_label.TabIndex = 5;
            this.fontColor_label.Text = "Font Color:";
            // 
            // fontColor_button
            // 
            this.fontColor_button.Location = new System.Drawing.Point(2, 83);
            this.fontColor_button.Margin = new System.Windows.Forms.Padding(0);
            this.fontColor_button.Name = "fontColor_button";
            this.fontColor_button.Size = new System.Drawing.Size(131, 27);
            this.fontColor_button.TabIndex = 4;
            this.fontColor_button.UseVisualStyleBackColor = true;
            this.fontColor_button.Click += new System.EventHandler(this.fontColor_button_Click);
            // 
            // backgroundColor_label
            // 
            this.backgroundColor_label.AutoSize = true;
            this.backgroundColor_label.Location = new System.Drawing.Point(2, 116);
            this.backgroundColor_label.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.backgroundColor_label.Name = "backgroundColor_label";
            this.backgroundColor_label.Size = new System.Drawing.Size(92, 40);
            this.backgroundColor_label.TabIndex = 3;
            this.backgroundColor_label.Text = "Background Color:";
            // 
            // backgroundColor_button
            // 
            this.backgroundColor_button.Location = new System.Drawing.Point(2, 156);
            this.backgroundColor_button.Margin = new System.Windows.Forms.Padding(0);
            this.backgroundColor_button.Name = "backgroundColor_button";
            this.backgroundColor_button.Size = new System.Drawing.Size(131, 27);
            this.backgroundColor_button.TabIndex = 2;
            this.backgroundColor_button.UseVisualStyleBackColor = true;
            this.backgroundColor_button.Click += new System.EventHandler(this.backgroundColor_button_Click);
            // 
            // layout_label
            // 
            this.layout_label.AutoSize = true;
            this.layout_label.Location = new System.Drawing.Point(2, 189);
            this.layout_label.Margin = new System.Windows.Forms.Padding(0, 6, 0, 2);
            this.layout_label.Name = "layout_label";
            this.layout_label.Size = new System.Drawing.Size(89, 20);
            this.layout_label.TabIndex = 6;
            this.layout_label.Text = "Layout type:";
            // 
            // layout_comboBox
            // 
            this.layout_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layout_comboBox.FormattingEnabled = true;
            this.layout_comboBox.Items.AddRange(new object[] {
            "Spiral",
            "Block"});
            this.layout_comboBox.Location = new System.Drawing.Point(2, 211);
            this.layout_comboBox.Margin = new System.Windows.Forms.Padding(0);
            this.layout_comboBox.Name = "layout_comboBox";
            this.layout_comboBox.Size = new System.Drawing.Size(131, 28);
            this.layout_comboBox.TabIndex = 2;
            this.layout_comboBox.SelectedIndexChanged += new System.EventHandler(this.layout_comboBox_SelectedIndexChanged);
            // 
            // growthPercent_label
            // 
            this.growthPercent_label.AutoSize = true;
            this.growthPercent_label.Location = new System.Drawing.Point(2, 245);
            this.growthPercent_label.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.growthPercent_label.Name = "growthPercent_label";
            this.growthPercent_label.Size = new System.Drawing.Size(103, 40);
            this.growthPercent_label.TabIndex = 7;
            this.growthPercent_label.Text = "Growth % per frequency";
            // 
            // growthPercent_numeric
            // 
            this.growthPercent_numeric.Location = new System.Drawing.Point(5, 288);
            this.growthPercent_numeric.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.growthPercent_numeric.Name = "growthPercent_numeric";
            this.growthPercent_numeric.Size = new System.Drawing.Size(128, 27);
            this.growthPercent_numeric.TabIndex = 1;
            this.growthPercent_numeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.growthPercent_numeric.ValueChanged += new System.EventHandler(this.growthPercent_numeric_ValueChanged);
            // 
            // imageWidth_label
            // 
            this.imageWidth_label.AutoSize = true;
            this.imageWidth_label.Location = new System.Drawing.Point(2, 324);
            this.imageWidth_label.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.imageWidth_label.Name = "imageWidth_label";
            this.imageWidth_label.Size = new System.Drawing.Size(98, 20);
            this.imageWidth_label.TabIndex = 9;
            this.imageWidth_label.Text = "Image Width:";
            // 
            // imageWidth_numeric
            // 
            this.imageWidth_numeric.Location = new System.Drawing.Point(5, 347);
            this.imageWidth_numeric.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.imageWidth_numeric.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.imageWidth_numeric.Name = "imageWidth_numeric";
            this.imageWidth_numeric.Size = new System.Drawing.Size(128, 27);
            this.imageWidth_numeric.TabIndex = 8;
            this.imageWidth_numeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.imageWidth_numeric.ValueChanged += new System.EventHandler(this.imageWidth_numeric_ValueChanged);
            // 
            // imageHeight_label
            // 
            this.imageHeight_label.AutoSize = true;
            this.imageHeight_label.Location = new System.Drawing.Point(2, 383);
            this.imageHeight_label.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.imageHeight_label.Name = "imageHeight_label";
            this.imageHeight_label.Size = new System.Drawing.Size(103, 20);
            this.imageHeight_label.TabIndex = 11;
            this.imageHeight_label.Text = "Image Height:";
            // 
            // imageHeight_numeric
            // 
            this.imageHeight_numeric.Location = new System.Drawing.Point(5, 406);
            this.imageHeight_numeric.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.imageHeight_numeric.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.imageHeight_numeric.Name = "imageHeight_numeric";
            this.imageHeight_numeric.Size = new System.Drawing.Size(128, 27);
            this.imageHeight_numeric.TabIndex = 10;
            this.imageHeight_numeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.imageHeight_numeric.ValueChanged += new System.EventHandler(this.imageHeight_numeric_ValueChanged);
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPictureBox.Location = new System.Drawing.Point(152, 12);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(683, 451);
            this.mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mainPictureBox.TabIndex = 1;
            this.mainPictureBox.TabStop = false;
            // 
            // generate_button
            // 
            this.generate_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.generate_button.Enabled = false;
            this.generate_button.Location = new System.Drawing.Point(12, 466);
            this.generate_button.Name = "generate_button";
            this.generate_button.Size = new System.Drawing.Size(133, 40);
            this.generate_button.TabIndex = 2;
            this.generate_button.Text = "Generate Image";
            this.generate_button.UseVisualStyleBackColor = true;
            this.generate_button.Click += new System.EventHandler(this.generate_button_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.saveImage_button);
            this.flowLayoutPanel2.Controls.Add(this.removeFilter_button);
            this.flowLayoutPanel2.Controls.Add(this.chooseFilterFile_button);
            this.flowLayoutPanel2.Controls.Add(this.inputFile_button);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(152, 466);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(683, 40);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // inputFile_button
            // 
            this.inputFile_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.inputFile_button.Location = new System.Drawing.Point(131, 0);
            this.inputFile_button.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.inputFile_button.Name = "inputFile_button";
            this.inputFile_button.Size = new System.Drawing.Size(133, 40);
            this.inputFile_button.TabIndex = 3;
            this.inputFile_button.Text = "Choose input file";
            this.inputFile_button.UseVisualStyleBackColor = true;
            this.inputFile_button.Click += new System.EventHandler(this.inputFile_button_Click);
            // 
            // chooseFilterFile_button
            // 
            this.chooseFilterFile_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chooseFilterFile_button.Location = new System.Drawing.Point(269, 0);
            this.chooseFilterFile_button.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.chooseFilterFile_button.Name = "chooseFilterFile_button";
            this.chooseFilterFile_button.Size = new System.Drawing.Size(133, 40);
            this.chooseFilterFile_button.TabIndex = 4;
            this.chooseFilterFile_button.Text = "Choose filter file";
            this.chooseFilterFile_button.UseVisualStyleBackColor = true;
            this.chooseFilterFile_button.Click += new System.EventHandler(this.chooseFilterFile_button_Click);
            // 
            // removeFilter_button
            // 
            this.removeFilter_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeFilter_button.Enabled = false;
            this.removeFilter_button.Location = new System.Drawing.Point(407, 0);
            this.removeFilter_button.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.removeFilter_button.Name = "removeFilter_button";
            this.removeFilter_button.Size = new System.Drawing.Size(133, 40);
            this.removeFilter_button.TabIndex = 5;
            this.removeFilter_button.Text = "Remove filter file";
            this.removeFilter_button.UseVisualStyleBackColor = true;
            this.removeFilter_button.Click += new System.EventHandler(this.removeFilter_button_Click);
            // 
            // saveImage_button
            // 
            this.saveImage_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveImage_button.Enabled = false;
            this.saveImage_button.Location = new System.Drawing.Point(545, 0);
            this.saveImage_button.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.saveImage_button.Name = "saveImage_button";
            this.saveImage_button.Size = new System.Drawing.Size(133, 40);
            this.saveImage_button.TabIndex = 6;
            this.saveImage_button.Text = "Save Image";
            this.saveImage_button.UseVisualStyleBackColor = true;
            this.saveImage_button.Click += new System.EventHandler(this.saveImage_button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 518);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.generate_button);
            this.Controls.Add(this.mainPictureBox);
            this.Controls.Add(this.flowLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(865, 565);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.growthPercent_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageWidth_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageHeight_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private FlowLayoutPanel flowLayoutPanel1;
    private Button fontButton;
    private FontDialog fontDialog;
    private ColorDialog colorDialog;
    private Button backgroundColor_button;
    private Label fontColor_label;
    private Label backgroundColor_label;
    private Button fontColor_button;
    private Label layout_label;
    private ComboBox layout_comboBox;
    private NumericUpDown growthPercent_numeric;
    private Label growthPercent_label;
    private Label imageWidth_label;
    private NumericUpDown imageWidth_numeric;
    private Label imageHeight_label;
    private NumericUpDown imageHeight_numeric;
    private PictureBox mainPictureBox;
    private Button generate_button;
    private FlowLayoutPanel flowLayoutPanel2;
    private Button inputFile_button;
    private Button chooseFilterFile_button;
    private Button removeFilter_button;
    private Button saveImage_button;
}