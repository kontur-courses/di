using System.ComponentModel;

namespace TagsCloudGUI
{
    partial class ExludedWordsForm
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.but_add = new System.Windows.Forms.Button();
            this.but_del = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 51);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(270, 407);
            this.listBox1.TabIndex = 0;
            // 
            // but_add
            // 
            this.but_add.Location = new System.Drawing.Point(288, 12);
            this.but_add.Name = "but_add";
            this.but_add.Size = new System.Drawing.Size(96, 23);
            this.but_add.TabIndex = 1;
            this.but_add.Text = "Добавить";
            this.but_add.UseVisualStyleBackColor = true;
            this.but_add.Click += new System.EventHandler(this.but_add_Click);
            // 
            // but_del
            // 
            this.but_del.Location = new System.Drawing.Point(288, 51);
            this.but_del.Name = "but_del";
            this.but_del.Size = new System.Drawing.Size(96, 23);
            this.but_del.TabIndex = 2;
            this.but_del.Text = "Удалить";
            this.but_del.UseVisualStyleBackColor = true;
            this.but_del.Click += new System.EventHandler(this.but_del_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(270, 20);
            this.textBox1.TabIndex = 3;
            // 
            // ExludedWordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 465);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.but_del);
            this.Controls.Add(this.but_add);
            this.Controls.Add(this.listBox1);
            this.Name = "ExludedWordsForm";
            this.Text = "ExludedWordsForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button but_add;
        private System.Windows.Forms.Button but_del;
        private System.Windows.Forms.TextBox textBox1;

        #endregion
    }
}