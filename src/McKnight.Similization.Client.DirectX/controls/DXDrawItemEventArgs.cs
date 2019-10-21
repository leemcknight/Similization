using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace LJM.Similization.Client.DirectX.Controls
{
    public class DXDrawItemEventArgs : EventArgs
    {
        private Color _foreColor;
        private Color _backColor;        
        private Rectangle _bounds;
        private bool _hotItem;
        private int _index;

        public DXDrawItemEventArgs(Color foreColor, Color backColor, Rectangle bounds, bool hotItem, int index)
        {
            _foreColor = foreColor;
            _backColor = backColor;
            _bounds = bounds;
            _hotItem = hotItem;
            _index = index;
        }

        public int Index
        {
            get { return _index; }
        }

        public Color ForeColor
        {
            get { return _foreColor; }
            set { _foreColor = value; }
        }

        public Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }

        public Rectangle Bounds
        {
            get { return _bounds; }
            set { _bounds = value; }
        }

        public bool IsHotItem
        {
            get { return _hotItem; }
            set { _hotItem = value; }
        }
    }
}
