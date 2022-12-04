using System.ComponentModel;

namespace TagsCloudVisualization
{
    partial class GetParamsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.rectanglesCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.centerX = new System.Windows.Forms.TextBox();
            this.centerY = new System.Windows.Forms.TextBox();
            this.generate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.spiralStep = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите количество прямоугольников";
            // 
            // rectanglesCount
            // 
            this.rectanglesCount.Location = new System.Drawing.Point(291, 14);
            this.rectanglesCount.Name = "rectanglesCount";
            this.rectanglesCount.Size = new System.Drawing.Size(120, 22);
            this.rectanglesCount.TabIndex = 1;
            this.rectanglesCount.Text = "60";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Введите координаты точки центра";
            // 
            // centerX
            // 
            this.centerX.Location = new System.Drawing.Point(292, 41);
            this.centerX.Name = "centerX";
            this.centerX.Size = new System.Drawing.Size(50, 22);
            this.centerX.TabIndex = 3;
            this.centerX.Text = "400";
            // 
            // centerY
            // 
            this.centerY.Location = new System.Drawing.Point(361, 41);
            this.centerY.Name = "centerY";
            this.centerY.Size = new System.Drawing.Size(50, 22);
            this.centerY.TabIndex = 4;
            this.centerY.Text = "250";
            // 
            // generate
            // 
            this.generate.Location = new System.Drawing.Point(12, 103);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(399, 32);
            this.generate.TabIndex = 5;
            this.generate.Text = "Создать облако случайных прямоугольников";
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.generate_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(262, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Введите шаг спирали";
            // 
            // textBox1
            // 
            this.spiralStep.Location = new System.Drawing.Point(291, 69);
            this.spiralStep.Name = "spiralStep";
            this.spiralStep.Size = new System.Drawing.Size(119, 22);
            this.spiralStep.TabIndex = 7;
            this.spiralStep.Text = "1";
            // 
            // GetParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 147);
            this.Controls.Add(this.spiralStep);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.generate);
            this.Controls.Add(this.centerY);
            this.Controls.Add(this.centerX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rectanglesCount);
            this.Controls.Add(this.label1);
            this.Name = "GetParamsForm";
            this.Text = "GetParamsForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox spiralStep;

        private System.Windows.Forms.TextBox centerX;
        private System.Windows.Forms.TextBox centerY;
        private System.Windows.Forms.Button generate;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.TextBox rectanglesCount;
        private System.Windows.Forms.Label label1;

        #endregion
    }
}