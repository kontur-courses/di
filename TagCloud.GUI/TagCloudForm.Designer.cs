namespace TagCloud.GUI
{
    partial class TagCloudForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.programmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTextFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStopwordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTagsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.OutputPanel = new System.Windows.Forms.Panel();
            this.ChangeColorButton = new System.Windows.Forms.Button();
            this.ResolutionLabel = new System.Windows.Forms.Label();
            this.ResolutionXLabel = new System.Windows.Forms.Label();
            this.YBox = new System.Windows.Forms.TextBox();
            this.XBox = new System.Windows.Forms.TextBox();
            this.ChangeFontButton = new System.Windows.Forms.Button();
            this.Format = new System.Windows.Forms.ListBox();
            this.ChangeFontColorButton = new System.Windows.Forms.Button();
            this.DrawButton = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.OutputPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programmToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(724, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // programmToolStripMenuItem
            // 
            this.programmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTextFileToolStripMenuItem,
            this.saveImageToolStripMenuItem,
            this.addStopwordsToolStripMenuItem,
            this.setTagsFileToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.programmToolStripMenuItem.Name = "programmToolStripMenuItem";
            this.programmToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.programmToolStripMenuItem.Text = "Programm";
            // 
            // loadTextFileToolStripMenuItem
            // 
            this.loadTextFileToolStripMenuItem.Name = "loadTextFileToolStripMenuItem";
            this.loadTextFileToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.loadTextFileToolStripMenuItem.Text = "Load text file";
            this.loadTextFileToolStripMenuItem.Click += new System.EventHandler(this.LoadTextFileToolStripMenuItem_Click);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.saveImageToolStripMenuItem.Text = "Set output image file";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.SaveImageToolStripMenuItem_Click);
            // 
            // addStopwordsToolStripMenuItem
            // 
            this.addStopwordsToolStripMenuItem.Name = "addStopwordsToolStripMenuItem";
            this.addStopwordsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.addStopwordsToolStripMenuItem.Text = "Set stop words file";
            this.addStopwordsToolStripMenuItem.Click += new System.EventHandler(this.SetStopwordsToolStripMenuItem_Click);
            // 
            // setTagsFileToolStripMenuItem
            // 
            this.setTagsFileToolStripMenuItem.Name = "setTagsFileToolStripMenuItem";
            this.setTagsFileToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.setTagsFileToolStripMenuItem.Text = "Set tags file";
            this.setTagsFileToolStripMenuItem.Click += new System.EventHandler(this.SetTagsFileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // Picture
            // 
            this.Picture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Picture.BackColor = System.Drawing.Color.White;
            this.Picture.Location = new System.Drawing.Point(12, 27);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(509, 387);
            this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Picture.TabIndex = 3;
            this.Picture.TabStop = false;
            // 
            // OutputLabel
            // 
            this.OutputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Location = new System.Drawing.Point(527, 218);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(52, 13);
            this.OutputLabel.TabIndex = 4;
            this.OutputLabel.Text = "OUTPUT";
            // 
            // OutputPanel
            // 
            this.OutputPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputPanel.Controls.Add(this.ChangeColorButton);
            this.OutputPanel.Controls.Add(this.ResolutionLabel);
            this.OutputPanel.Controls.Add(this.ResolutionXLabel);
            this.OutputPanel.Controls.Add(this.YBox);
            this.OutputPanel.Controls.Add(this.XBox);
            this.OutputPanel.Controls.Add(this.ChangeFontButton);
            this.OutputPanel.Controls.Add(this.Format);
            this.OutputPanel.Controls.Add(this.ChangeFontColorButton);
            this.OutputPanel.Location = new System.Drawing.Point(527, 27);
            this.OutputPanel.Name = "OutputPanel";
            this.OutputPanel.Size = new System.Drawing.Size(185, 188);
            this.OutputPanel.TabIndex = 5;
            // 
            // ChangeColorButton
            // 
            this.ChangeColorButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ChangeColorButton.Location = new System.Drawing.Point(3, 123);
            this.ChangeColorButton.Name = "ChangeColorButton";
            this.ChangeColorButton.Size = new System.Drawing.Size(179, 23);
            this.ChangeColorButton.TabIndex = 15;
            this.ChangeColorButton.Text = "Change Background Color";
            this.ChangeColorButton.UseVisualStyleBackColor = true;
            this.ChangeColorButton.Click += new System.EventHandler(this.ChangeBackgroundColorButton_Click);
            // 
            // ResolutionLabel
            // 
            this.ResolutionLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ResolutionLabel.AutoSize = true;
            this.ResolutionLabel.Location = new System.Drawing.Point(66, 148);
            this.ResolutionLabel.Name = "ResolutionLabel";
            this.ResolutionLabel.Size = new System.Drawing.Size(57, 13);
            this.ResolutionLabel.TabIndex = 14;
            this.ResolutionLabel.Text = "Resolution";
            // 
            // ResolutionXLabel
            // 
            this.ResolutionXLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ResolutionXLabel.AutoSize = true;
            this.ResolutionXLabel.Location = new System.Drawing.Point(89, 167);
            this.ResolutionXLabel.Name = "ResolutionXLabel";
            this.ResolutionXLabel.Size = new System.Drawing.Size(12, 13);
            this.ResolutionXLabel.TabIndex = 12;
            this.ResolutionXLabel.Text = "x";
            // 
            // YBox
            // 
            this.YBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.YBox.Location = new System.Drawing.Point(107, 164);
            this.YBox.Name = "YBox";
            this.YBox.Size = new System.Drawing.Size(43, 20);
            this.YBox.TabIndex = 11;
            this.YBox.Text = "100";
            this.YBox.TextChanged += new System.EventHandler(this.YBox_TextChanged);
            // 
            // XBox
            // 
            this.XBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.XBox.Location = new System.Drawing.Point(40, 164);
            this.XBox.Name = "XBox";
            this.XBox.Size = new System.Drawing.Size(43, 20);
            this.XBox.TabIndex = 10;
            this.XBox.Text = "100";
            this.XBox.TextChanged += new System.EventHandler(this.XBox_TextChanged);
            // 
            // ChangeFontButton
            // 
            this.ChangeFontButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ChangeFontButton.Location = new System.Drawing.Point(3, 94);
            this.ChangeFontButton.Name = "ChangeFontButton";
            this.ChangeFontButton.Size = new System.Drawing.Size(179, 23);
            this.ChangeFontButton.TabIndex = 9;
            this.ChangeFontButton.Text = "Change Font";
            this.ChangeFontButton.UseVisualStyleBackColor = true;
            this.ChangeFontButton.Click += new System.EventHandler(this.ChangeFontButton_Click);
            // 
            // Format
            // 
            this.Format.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Format.FormattingEnabled = true;
            this.Format.Location = new System.Drawing.Point(0, 3);
            this.Format.Name = "Format";
            this.Format.Size = new System.Drawing.Size(182, 56);
            this.Format.TabIndex = 8;
            this.Format.SelectedIndexChanged += new System.EventHandler(this.Format_SelectedIndexChanged);
            // 
            // ChangeFontColorButton
            // 
            this.ChangeFontColorButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ChangeFontColorButton.Location = new System.Drawing.Point(3, 65);
            this.ChangeFontColorButton.Name = "ChangeFontColorButton";
            this.ChangeFontColorButton.Size = new System.Drawing.Size(179, 23);
            this.ChangeFontColorButton.TabIndex = 6;
            this.ChangeFontColorButton.Text = "Change Font Color";
            this.ChangeFontColorButton.UseVisualStyleBackColor = true;
            this.ChangeFontColorButton.Click += new System.EventHandler(this.ChangeFontColorButton_Click);
            // 
            // DrawButton
            // 
            this.DrawButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawButton.Location = new System.Drawing.Point(581, 397);
            this.DrawButton.Name = "DrawButton";
            this.DrawButton.Size = new System.Drawing.Size(75, 23);
            this.DrawButton.TabIndex = 6;
            this.DrawButton.Text = "DRAW";
            this.DrawButton.UseVisualStyleBackColor = true;
            this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
            // 
            // Output
            // 
            this.Output.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Output.Location = new System.Drawing.Point(527, 234);
            this.Output.Multiline = true;
            this.Output.Name = "Output";
            this.Output.ReadOnly = true;
            this.Output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Output.Size = new System.Drawing.Size(185, 161);
            this.Output.TabIndex = 9;
            // 
            // TagCloudForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(724, 426);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.DrawButton);
            this.Controls.Add(this.OutputPanel);
            this.Controls.Add(this.Picture);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(740, 465);
            this.Name = "TagCloudForm";
            this.Text = "Tag Cloud";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.OutputPanel.ResumeLayout(false);
            this.OutputPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem programmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.Panel OutputPanel;
        private System.Windows.Forms.ToolStripMenuItem loadTextFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.Button ChangeFontColorButton;
        private System.Windows.Forms.ListBox Format;
        private System.Windows.Forms.Button ChangeFontButton;
        private System.Windows.Forms.ToolStripMenuItem addStopwordsToolStripMenuItem;
        private System.Windows.Forms.Label ResolutionXLabel;
        private System.Windows.Forms.TextBox YBox;
        private System.Windows.Forms.TextBox XBox;
        private System.Windows.Forms.Button DrawButton;
        private System.Windows.Forms.Label ResolutionLabel;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.ToolStripMenuItem setTagsFileToolStripMenuItem;
        private System.Windows.Forms.Button ChangeColorButton;
    }
}

