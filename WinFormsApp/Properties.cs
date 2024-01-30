using TagsCloudContainer.SettingsClasses;
using TagsCloudVisualization;

namespace WinFormsApp
{
    public class Properties : Form
    {
        private Label widthLabel;
        private NumericUpDown widthNumericUpDown;
        private NumericUpDown heightNumericUpDown;
        private Button okButton;
        private Label heightLabel;
        private Button fontSelectionButton;
        private Button filterFileButton;
        private Label excludeFileLabel;
        private ComboBox comboBox1;
        private Label cloudPatternLabel;
        private Button cancelButton;
        private readonly List<IPointsProvider> providers;
        public AppSettings appSettings { get; private set; }


        public Properties(AppSettings appSettings, List<IPointsProvider> providers)
        {
            this.appSettings = appSettings;
            this.providers = providers;
            InitializeComponent();

            LoadSettings(appSettings, providers);
        }

        private void LoadSettings(AppSettings appSettings, List<IPointsProvider> providers)
        {
            foreach (var provider in providers)
            {
                comboBox1.Items.Insert(0, provider);
            }
            comboBox1.SelectedIndex = 0;

            widthNumericUpDown.Value = appSettings.DrawingSettings.Size.Width;
            heightNumericUpDown.Value = appSettings.DrawingSettings.Size.Height;
        }

        private void InitializeComponent()
        {
            widthLabel = new Label();
            widthNumericUpDown = new NumericUpDown();
            okButton = new Button();
            cancelButton = new Button();
            heightNumericUpDown = new NumericUpDown();
            heightLabel = new Label();
            fontSelectionButton = new Button();
            filterFileButton = new Button();
            excludeFileLabel = new Label();
            comboBox1 = new ComboBox();
            cloudPatternLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)widthNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heightNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // widthLabel
            // 
            widthLabel.Location = new Point(10, 10);
            widthLabel.Name = "widthLabel";
            widthLabel.Size = new Size(142, 20);
            widthLabel.TabIndex = 2;
            widthLabel.Text = "Width:";
            // 
            // widthNumericUpDown
            // 
            widthNumericUpDown.Location = new Point(12, 33);
            widthNumericUpDown.Maximum = new decimal(new int[] { 1500, 0, 0, 0 });
            widthNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            widthNumericUpDown.Name = "widthNumericUpDown";
            widthNumericUpDown.Size = new Size(140, 27);
            widthNumericUpDown.TabIndex = 3;
            widthNumericUpDown.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // okButton
            // 
            okButton.Location = new Point(10, 234);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 25);
            okButton.TabIndex = 0;
            okButton.Text = "OK";
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(223, 234);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 25);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.Click += CancelButton_Click;
            // 
            // heightNumericUpDown
            // 
            heightNumericUpDown.Location = new Point(158, 33);
            heightNumericUpDown.Maximum = new decimal(new int[] { 1500, 0, 0, 0 });
            heightNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            heightNumericUpDown.Name = "heightNumericUpDown";
            heightNumericUpDown.Size = new Size(140, 27);
            heightNumericUpDown.TabIndex = 3;
            heightNumericUpDown.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // heightLabel
            // 
            heightLabel.Location = new Point(156, 10);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(142, 20);
            heightLabel.TabIndex = 2;
            heightLabel.Text = "Height:";
            // 
            // fontSelectionButton
            // 
            fontSelectionButton.Location = new Point(204, 102);
            fontSelectionButton.Name = "fontSelectionButton";
            fontSelectionButton.Size = new Size(94, 29);
            fontSelectionButton.TabIndex = 4;
            fontSelectionButton.Text = "Select Font";
            fontSelectionButton.UseVisualStyleBackColor = true;
            fontSelectionButton.Click += fontSelectionButton_Click;
            // 
            // filterFileButton
            // 
            filterFileButton.Location = new Point(10, 102);
            filterFileButton.Name = "filterFileButton";
            filterFileButton.Size = new Size(94, 29);
            filterFileButton.TabIndex = 4;
            filterFileButton.Text = "Exclude file";
            filterFileButton.UseVisualStyleBackColor = true;
            filterFileButton.Click += filterFileButton_Click;
            // 
            // excludeFileLabel
            // 
            excludeFileLabel.AutoSize = true;
            excludeFileLabel.Location = new Point(12, 79);
            excludeFileLabel.Name = "excludeFileLabel";
            excludeFileLabel.Size = new Size(38, 20);
            excludeFileLabel.TabIndex = 5;
            excludeFileLabel.Text = "<...>";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 185);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(286, 28);
            comboBox1.TabIndex = 6;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // cloudPatternLabel
            // 
            cloudPatternLabel.Location = new Point(12, 149);
            cloudPatternLabel.Name = "cloudPatternLabel";
            cloudPatternLabel.Size = new Size(286, 33);
            cloudPatternLabel.TabIndex = 2;
            cloudPatternLabel.Text = "Tag Cloud Layout Pattern";
            // 
            // Properties
            // 
            ClientSize = new Size(319, 276);
            Controls.Add(comboBox1);
            Controls.Add(excludeFileLabel);
            Controls.Add(filterFileButton);
            Controls.Add(fontSelectionButton);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Controls.Add(heightLabel);
            Controls.Add(cloudPatternLabel);
            Controls.Add(widthLabel);
            Controls.Add(heightNumericUpDown);
            Controls.Add(widthNumericUpDown);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Properties";
            Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)widthNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)heightNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // Handle OK button click
            appSettings.DrawingSettings.Size = new Size((int)widthNumericUpDown.Value, (int)heightNumericUpDown.Value);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // Handle Cancel button click
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void fontSelectionButton_Click(object sender, EventArgs e)
        {
            var fontDialog = new FontDialog();

            var font = new Font(appSettings.DrawingSettings.FontFamily, appSettings.DrawingSettings.FontSize);
            fontDialog.Font = font;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                appSettings.DrawingSettings.FontFamily = fontDialog.Font.FontFamily;
                appSettings.DrawingSettings.FontSize = fontDialog.Font.Size;
            }
        }

        private void filterFileButton_Click(object sender, EventArgs e)
        {
            var openFile = new OpenFileDialog();
            openFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                appSettings.FilterFile = openFile.FileName;
                excludeFileLabel.Text = appSettings.FilterFile;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            appSettings.DrawingSettings.PointsProvider = (IPointsProvider)comboBox1.SelectedItem;
        }
    }
}
