using System.Data;

namespace WinFormsApp.SettingsForms
{
    public class ColorSelectorForm : Form
    {
        private ColorDialog colorDialog;

        private Button okButton;
        private Button cancelButton;
        private DataGridView dataGridView;
        public List<Color> Colors { get; private set; }
        private Button addColorButton;
        private Button removeColorButton;

        public ColorSelectorForm(ICollection<Color> colors)
        {
            Colors = colors.ToList();

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Initialize components here
            okButton = new Button();
            cancelButton = new Button();
            dataGridView = CreateDataGridView();

            // Set properties for okButton
            okButton.Text = "OK";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Location = new System.Drawing.Point(20, 320);
            okButton.Click += new System.EventHandler(this.OkButton_Click);

            // Set properties for cancelButton
            cancelButton.Text = "Cancel";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Location = new System.Drawing.Point(200, 320);
            cancelButton.Click += new System.EventHandler(this.CancelButton_Click);

            // Set properties for addColorButton
            addColorButton = new Button();
            addColorButton.Text = "Add Color";
            addColorButton.Size = new System.Drawing.Size(75, 23);
            addColorButton.Location = new System.Drawing.Point(40, 280);
            addColorButton.Click += new System.EventHandler(this.AddColorButton_Click);

            // Set properties for removeColorButton
            removeColorButton = new Button();
            removeColorButton.Text = "Remove Color";
            removeColorButton.Size = new System.Drawing.Size(75, 23);
            removeColorButton.Location = new System.Drawing.Point(140, 280);
            removeColorButton.Click += new System.EventHandler(this.RemoveColorButton_Click);

            // Add items to the form
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Controls.Add(dataGridView);
            Controls.Add(addColorButton);
            Controls.Add(removeColorButton);

            // Set form properties
            Text = "Color Selector";
            Width = 300;
            Height = 400;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            // Add data to the DataGridView
            var table = new DataTable();
            table.Columns.Add("Color", typeof(Color));
            dataGridView.DataSource = table;

            // Bind color items to the DataGridView
            foreach (var colorItem in Colors)
            {
                table.Rows.Add(colorItem);
            }
        }

        private DataGridView CreateDataGridView()
        {
            var dgv = new DataGridView();
            dgv.Location = new System.Drawing.Point(10, 10);
            dgv.Size = new System.Drawing.Size(265, 250);
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
    }
}
