using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHControls.Buttons
{
    class GradationButton : Button
    {
        private Color _GradationStartColor = Color.Green;
        [Description("Gradation Start Color"), Category("Gradation")]
        public Color GradationStartColor
        {
            get { return _GradationStartColor; }
            set { _GradationStartColor = value; this.Refresh(); }
        }
        private Color _GradationEndColor = Color.Yellow;
        [Description("Gradation End Color"), Category("Gradation")]
        public Color GradatinEndColor
        {
            get { return _GradationEndColor; }
            set { _GradationEndColor = value; this.Refresh(); }
        }

        private float _GradationRatio = 0;
        [Description("Gradation Ratio"), Category("Gradation")]
        public float GradtionRatio
        {
            get { return _GradationRatio; }
            set { _GradationRatio = value; this.Refresh(); }
        }

        private Color _BorderColor = Color.Black;
        [Description("Border Color"), Category("Gradation")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; this.Refresh(); }
        }
        private int _BorderSize = 1;
        [Description("Border Size ( 0 ~ 5 ) "), Category("Gradation")]
        public int BorderSize
        {
            get { return _BorderSize; }
            set
            {
                _BorderSize = value;
                if (_BorderSize < 0) _BorderSize = 0;
                if (_BorderSize > 5) _BorderSize = 5;
                this.Refresh();
            }
        }

        private bool _MouseHovered = false;


        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Width <= 0 || this.Height <= 0) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            LinearGradientBrush linGrBrush;

            if(!_MouseHovered)
               linGrBrush = new LinearGradientBrush(this.ClientRectangle, _GradationStartColor, _GradationEndColor, _GradationRatio);  
            else
                linGrBrush = new LinearGradientBrush(this.ClientRectangle,
                    MouseHoveredColor(_GradationStartColor), MouseHoveredColor(_GradationEndColor), _GradationRatio);

            int sx = _BorderSize;
            int sy = _BorderSize;
            int width = this.Width - _BorderSize * 2;
            int height = this.Height - _BorderSize * 2;
            if (width > 0 && height > 0)
            {
                Rectangle backgroundRectangle = new Rectangle(sx, sy, width, height);
                Rectangle textRectangle = new Rectangle(sx + this.Padding.Left, sy + this.Padding.Top, width - this.Padding.Left - this.Padding.Right, height - this.Padding.Top - this.Padding.Bottom);

                StringFormat sf = GetThisStringForamt();
                e.Graphics.FillRectangle(linGrBrush,backgroundRectangle);
                e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRectangle, sf);
            }

            if (_BorderSize != 0)
            {
                Pen pen = new Pen(BorderColor);
                pen.Width = _BorderSize;
                e.Graphics.DrawLine(pen, new Point(0, 0), new Point(0, this.Height - 1));
                e.Graphics.DrawLine(pen, new Point(0, 0), new Point(this.Width - 1, 0));
                e.Graphics.DrawLine(pen, new Point(this.Width - 1, this.Height - 1), new Point(0, this.Height - 1));
                e.Graphics.DrawLine(pen, new Point(this.Width - 1, this.Height - 1), new Point(this.Width - 1, 0));
            }
        }

        #region [ Mouse Hover Color ]
        protected override void OnMouseHover(EventArgs e)
        {
            _MouseHovered = true ;
            base.OnMouseHover(e);
            this.Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _MouseHovered = false;
            base.OnMouseLeave(e);
            this.Refresh();
        }

        private Color MouseHoveredColor(Color color)
        {
            int r = GetHonvertedInt(color.R);
            int g = GetHonvertedInt(color.G);
            int b = GetHonvertedInt(color.B);

            return Color.FromArgb(r, g, b);
        }
        private int GetHonvertedInt(int colorValue)
        {
            if (colorValue < 50) return colorValue + 30;
            else return colorValue - 30;
        }
        #endregion


        private StringFormat GetThisStringForamt()
        {
            StringFormat thisFormat = new StringFormat();
            switch(this.TextAlign)
            {
                case ContentAlignment.BottomCenter:
                    thisFormat.LineAlignment = StringAlignment.Far;
                    thisFormat.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    thisFormat.LineAlignment = StringAlignment.Far;
                    thisFormat.Alignment = StringAlignment.Near;
                    break;

                case ContentAlignment.BottomRight:
                    thisFormat.LineAlignment = StringAlignment.Far;
                    thisFormat.Alignment = StringAlignment.Far;
                    break;

                case ContentAlignment.MiddleCenter:
                    thisFormat.LineAlignment = StringAlignment.Center;
                    thisFormat.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleLeft:
                    thisFormat.LineAlignment = StringAlignment.Center;
                    thisFormat.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleRight:
                    thisFormat.LineAlignment = StringAlignment.Center;
                    thisFormat.Alignment = StringAlignment.Far;
                    break;

                case ContentAlignment.TopCenter:
                    thisFormat.LineAlignment = StringAlignment.Near;
                    thisFormat.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopLeft:
                    thisFormat.LineAlignment = StringAlignment.Near;
                    thisFormat.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    thisFormat.LineAlignment = StringAlignment.Near;
                    thisFormat.Alignment = StringAlignment.Far;
                    break;
            }
            return thisFormat;
        }
    }
}
