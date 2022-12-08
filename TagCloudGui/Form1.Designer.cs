
namespace TagCloudGui
{
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadWordsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualizationSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagCloudPictureBox = new System.Windows.Forms.PictureBox();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tagCloudPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMenuItem,
            this.pictureMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(504, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "mainMenuStrip";
            // 
            // menuMenuItem
            // 
            this.menuMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadWordsMenuItem});
            this.menuMenuItem.Name = "menuMenuItem";
            this.menuMenuItem.Size = new System.Drawing.Size(53, 20);
            this.menuMenuItem.Text = "Меню";
            // 
            // loadWordsMenuItem
            // 
            this.loadWordsMenuItem.Name = "loadWordsMenuItem";
            this.loadWordsMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadWordsMenuItem.Text = "Загрузить слова";
            this.loadWordsMenuItem.Click += new System.EventHandler(this.loadWordsMenuItem_Click);
            // 
            // pictureMenuItem
            // 
            this.pictureMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visualizationSettingsMenuItem,
            this.saveAsMenuItem});
            this.pictureMenuItem.Name = "pictureMenuItem";
            this.pictureMenuItem.Size = new System.Drawing.Size(70, 20);
            this.pictureMenuItem.Text = "Картинка";
            // 
            // visualizationSettingsMenuItem
            // 
            this.visualizationSettingsMenuItem.Name = "visualizationSettingsMenuItem";
            this.visualizationSettingsMenuItem.Size = new System.Drawing.Size(211, 22);
            this.visualizationSettingsMenuItem.Text = "Настройки отображения";
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Name = "saveAsMenuItem";
            this.saveAsMenuItem.Size = new System.Drawing.Size(211, 22);
            this.saveAsMenuItem.Text = "Сохранить как...";
            // 
            // tagCloudPictureBox
            // 
            this.tagCloudPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagCloudPictureBox.Location = new System.Drawing.Point(0, 24);
            this.tagCloudPictureBox.Name = "tagCloudPictureBox";
            this.tagCloudPictureBox.Size = new System.Drawing.Size(504, 496);
            this.tagCloudPictureBox.TabIndex = 1;
            this.tagCloudPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 520);
            this.Controls.Add(this.tagCloudPictureBox);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Облако тегов";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tagCloudPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadWordsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pictureMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visualizationSettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
        private System.Windows.Forms.PictureBox tagCloudPictureBox;
    }
}

