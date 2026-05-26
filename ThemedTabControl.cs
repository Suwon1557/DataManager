using System.Drawing;
using System.Windows.Forms;

namespace DataManager
{
    internal class ThemedTabControl : TabControl
    {
        private readonly Color _back = Color.FromArgb(18, 24, 38);
        private readonly Color _tab = Color.FromArgb(39, 50, 72);
        private readonly Color _selected = Color.FromArgb(245, 176, 65);
        private readonly Color _text = Color.FromArgb(238, 243, 249);
        private readonly Color _selectedText = Color.FromArgb(31, 41, 55);

        public ThemedTabControl()
        {
            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.ResizeRedraw,
                true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(_back);

            for (int i = 0; i < TabCount; i++)
            {
                Rectangle tabRect = GetTabRect(i);
                bool selected = i == SelectedIndex;

                using (SolidBrush bg = new SolidBrush(selected ? _selected : _tab))
                    e.Graphics.FillRectangle(bg, tabRect);
                using (Pen border = new Pen(_back))
                    e.Graphics.DrawRectangle(border, tabRect);

                TextRenderer.DrawText(
                    e.Graphics,
                    TabPages[i].Text,
                    Font,
                    tabRect,
                    selected ? _selectedText : _text,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
        }
    }
}
