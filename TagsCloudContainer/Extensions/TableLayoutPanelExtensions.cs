using System.Collections.Generic;
using System.Windows.Forms;

namespace TagsCloudContainer.Extensions
{
    public static class TableLayoutPanelExtensions
    {
        public static void AddControls(
            this TableLayoutPanel table, IReadOnlyList<Control> controls, int column,
            int rowFrom, int rowSpan = 1)
        {
            for (var i = 0; i < controls.Count; i++)
            {
                controls[i].Dock = DockStyle.Fill;
                table.Controls.Add(controls[i], column, (rowFrom + i) * rowSpan);
                table.SetRowSpan(controls[i], rowSpan);
            }
        }

        public static void AddRows(this TableLayoutPanel table, int count, SizeType sizeType, int size)
        {
            for (var i = 0; i < count; i++)
            {
                table.RowStyles.Add(new RowStyle(sizeType, size));
            }
        }

        public static void AddControlsToRows(
            this TableLayoutPanel table, IReadOnlyList<Control> controls, int column, int rowFrom,
            SizeType rowSizeType, int rowSize, int rowSpan = 1)
        {
            table.AddRows(controls.Count * rowSpan, rowSizeType, rowSize);
            table.AddControls(controls, column, rowFrom, rowSpan);
        }
    }
}