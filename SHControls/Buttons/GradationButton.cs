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
        #region [ Property ]
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
        #endregion

        #region [ Members ]

        private bool _MouseHovered = false;
        private bool _MouseDowned = false;

        #endregion

        #region [ Paint ] 
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Width <= 0 || this.Height <= 0) return;

            SetGraphicHits(e);

            int width = this.Width - _BorderSize * 2;
            int height = this.Height - _BorderSize * 2;

            if (width > 0 && height > 0)
            {
                FillGradation(e, width, height);
                DrawText(e, width, height);
            }

            if (_BorderSize != 0) DrawOutLine(e);
        }

        private static void SetGraphicHits(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }

        private void FillGradation(PaintEventArgs e, int width, int height)
        {
            LinearGradientBrush linGrBrush = GetGradationBrush();

            Rectangle backgroundRectangle = new Rectangle(_BorderSize, _BorderSize, width, height);
            e.Graphics.FillRectangle(linGrBrush, backgroundRectangle);
        }

        private LinearGradientBrush GetGradationBrush()
        {
            LinearGradientBrush brush;

            if (_MouseDowned)
                brush = new LinearGradientBrush(this.ClientRectangle,
                    MouseDowneddColor(_GradationStartColor), MouseDowneddColor(_GradationEndColor), _GradationRatio);
            else if ( _MouseHovered)
                brush = new LinearGradientBrush(this.ClientRectangle,
                    MouseHoveredColor(_GradationStartColor), MouseHoveredColor(_GradationEndColor), _GradationRatio);
            else
                brush = new LinearGradientBrush(this.ClientRectangle, _GradationStartColor, _GradationEndColor, _GradationRatio);

            return brush;
        }

        private void DrawText(PaintEventArgs e, int width, int height)
        {
            Rectangle textRectangle = new Rectangle(_BorderSize + this.Padding.Left, _BorderSize + this.Padding.Top, width - this.Padding.Left - this.Padding.Right, height - this.Padding.Top - this.Padding.Bottom);

            StringFormat sf = GetThisStringForamt();
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRectangle, sf);
        }

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

        private void DrawOutLine(PaintEventArgs e)
        {
            Pen pen = new Pen(BorderColor);
            pen.Width = _BorderSize;
            e.Graphics.DrawLine(pen, new Point(0, 0), new Point(0, this.Height - 1));
            e.Graphics.DrawLine(pen, new Point(0, 0), new Point(this.Width - 1, 0));
            e.Graphics.DrawLine(pen, new Point(this.Width - 1, this.Height - 1), new Point(0, this.Height - 1));
            e.Graphics.DrawLine(pen, new Point(this.Width - 1, this.Height - 1), new Point(this.Width - 1, 0));
        }
        #endregion

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

        #region [ Mouse Down Color ]
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
        }

        private Color MouseDowneddColor(Color color)
        {
            int r = GetMouseDownedInt(color.R);
            int g = GetMouseDownedInt(color.G);
            int b = GetMouseDownedInt(color.B);

            return Color.FromArgb(r, g, b);
        }
        private int GetMouseDownedInt(int colorValue)
        {
            if (colorValue < 50) return colorValue + 80;
            else return colorValue - 45;
        }
        #endregion
    }
}
