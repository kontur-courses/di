using System.Data;

namespace WinFormsApp.SettingsForms
{
    public class ColorSelectorForm : Form
    {
        private Button okButton;
        private Button cancelButton;
        private Button bgColorButton;
        private DataGridView dataGridView;
        public List<Color> Colors { get; private set; }
        public Color BGColor { get; private set; }
        private Button addColorButton;
        private Button removeColorButton;

        public ColorSelectorForm(ICollection<Color> colors, Color bGColor)
        {
            Colors = colors.ToList();
            BGColor = bGColor;

            InitializeComponent();


            // Add data to the DataGridView
            var table = new DataTable();
            table.Columns.Add("Color", typeof(Color));
            dataGridView.DataSource = table;

            // Bind color items to the DataGridView
            foreach (var colorItem in Colors)
            {
                table.Rows.Add(colorItem);
            }

            bgColorButton.BackColor = BGColor;
        }

        private void InitializeComponent()
        {
            okButton = new Button();
            cancelButton = new Button();
            addColorButton = new Button();
            removeColorButton = new Button();
            bgColorButton = new Button();
            dataGridView = CreateDataGridView();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Location = new Point(12, 418);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 0;
            okButton.Text = "OK";
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(192, 418);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.Click += CancelButton_Click;
            // 
            // addColorButton
            // 
            addColorButton.Location = new Point(40, 280);
            addColorButton.Name = "addColorButton";
            addColorButton.Size = new Size(75, 23);
            addColorButton.TabIndex = 2;
            addColorButton.Text = "Add Color";
            addColorButton.Click += AddColorButton_Click;
            // 
            // removeColorButton
            // 
            removeColorButton.Location = new Point(140, 280);
            removeColorButton.Name = "removeColorButton";
            removeColorButton.Size = new Size(75, 23);
            removeColorButton.TabIndex = 3;
            removeColorButton.Text = "Remove Color";
            removeColorButton.Click += RemoveColorButton_Click;
            // 
            // bgColorButton
            // 
            bgColorButton.Location = new Point(94, 321);
            bgColorButton.Name = "bgColorButton";
            bgColorButton.Size = new Size(75, 75);
            bgColorButton.TabIndex = 1;
            bgColorButton.Text = "Background";
            bgColorButton.Click += BGButton_Click;
            // 
            // ColorSelectorForm
            // 
            ClientSize = new Size(282, 453);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Controls.Add(addColorButton);
            Controls.Add(removeColorButton);
            Controls.Add(bgColorButton);
            Controls.Add(dataGridView);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ColorSelectorForm";
            Text = "Color Selector";
            ResumeLayout(false);
        }

        private DataGridView CreateDataGridView()
        {
            var dgv = new DataGridView();
            dgv.Location = new Point(10, 10);
            dgv.Size = new Size(265, 250);
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = true;
            dgv.AllowUserToResizeColumns = true;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);

            return dgv;
        }

        private void AddColorButton_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Colors.Add(colorDialog.Color);
                var table = (DataTable)dataGridView.DataSource;
                table.Rows.Add(colorDialog.Color);
            }
        }

        private void RemoveColorButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0 && dataGridView.Rows.Count > 1)
            {
                Colors.RemoveAt(dataGridView.SelectedRows[0].Index);
                dataGridView.Rows.RemoveAt(dataGridView.SelectedRows[0].Index);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var color = (Color)dataGridView.Rows[e.RowIndex].Cells[0].Value;

                if (color != null)
                    e.CellStyle.BackColor = color;
            }
        }

        private void BGButton_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                BGColor = colorDialog.Color;
                bgColorButton.BackColor = BGColor;
            }
        }
    }
}
