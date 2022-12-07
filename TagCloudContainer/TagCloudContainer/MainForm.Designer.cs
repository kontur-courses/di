namespace TagCloudContainer;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Name = "MainForm";
        this.Text = "Tag cloud container";
        this.Paint += new System.Windows.Forms.PaintEventHandler(this.Render);
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}