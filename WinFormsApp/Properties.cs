using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp
{
    public class Properties : Form
    {
        private Label sizeLabel;
        private NumericUpDown widthNumericUpDown;
        private NumericUpDown heighNumericUpDown;
        private Button okButton;
        private Button cancelButton;

        public Properties()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Initialize components here
            // ...

            // Set form properties
            this.Text = "Settings";
            this.Width = 300;
            this.Height = 200;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Set properties for sizeLabel
            sizeLabel = new Label();
            sizeLabel.Text = "Size:";
            sizeLabel.Location = new System.Drawing.Point(10, 10);
            sizeLabel.Size = new System.Drawing.Size(40, 20);

            // Set properties for sizeTextBox
            widthNumericUpDown = new NumericUpDown();
            widthNumericUpDown.Location = new System.Drawing.Point(50, 10);
            widthNumericUpDown.Size = new System.Drawing.Size(140, 20);
            widthNumericUpDown.Minimum = 1;
            widthNumericUpDown.Maximum = 1000;

            // Set properties for okButton
            okButton = new Button();
            okButton.Text = "OK";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Location = new System.Drawing.Point(10, 40);
            okButton.Click += new System.EventHandler(this.OkButton_Click);

            // Set properties for cancelButton
            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Location = new System.Drawing.Point(90, 40);
            cancelButton.Click += new System.EventHandler(this.CancelButton_Click);

            // Add components to the form
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);
            this.Controls.Add(sizeLabel);
            this.Controls.Add(widthNumericUpDown);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // Handle OK button click
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // Handle Cancel button click
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
