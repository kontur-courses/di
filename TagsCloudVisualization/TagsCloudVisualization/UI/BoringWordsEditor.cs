using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization
{
    public class BoringWordsEditor : Form
    {
        private readonly DataGridView dataGridView;
        private readonly Button deleteWordButton;
        private readonly IBoringWordsProvider boringWordsProvider;

        public BoringWordsEditor(IBoringWordsProvider boringWordsProvider)
        {
            this.boringWordsProvider = boringWordsProvider;
            dataGridView = InitializeDataGrid(boringWordsProvider.BoringWords);
            deleteWordButton = InitializeDeleteButton();
            ResizeEditorComponents();
            Controls.Add(dataGridView);
            Controls.Add(deleteWordButton);
        }

        protected override void OnResize(EventArgs e)
        {
            ResizeEditorComponents();
        }

        private void ResizeEditorComponents()
        {
            deleteWordButton.Size = new Size(ClientSize.Width, 50);
            dataGridView.Size = new Size(ClientSize.Width, ClientSize.Height - deleteWordButton.Height);
            deleteWordButton.Location = new Point(0, ClientSize.Height - deleteWordButton.Height);
        }

        private void RemoveBoringWord(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow != null && !dataGridView.CurrentRow.IsNewRow)
                dataGridView.Rows.Remove(dataGridView.CurrentRow);
        }

        private Button InitializeDeleteButton()
        {
            var button = new Button {Text = "Delete Current Word"};
            button.Click += RemoveBoringWord;
            button.Size = new Size(ClientSize.Width, 50);
            return button;
        }

        private DataGridView InitializeDataGrid(IEnumerable<string> boringWords)
        {
            var dataGrid = new DataGridView();
            dataGrid.Columns.Add("Column", "Boring Word");
            dataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            foreach (var boringWord in boringWords)
                dataGrid.Rows.Add(boringWord);
            return dataGrid;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            boringWordsProvider.BoringWords = ConvertDataGridToEnumerable().ToHashSet();
        }

        private IEnumerable<string> ConvertDataGridToEnumerable()
        {
            foreach (DataGridViewRow objRow in dataGridView.Rows)
            {
                var boringWord = objRow.Cells[0].Value as string;
                if (boringWord != string.Empty)
                    yield return boringWord;
            }
        }
    }
}