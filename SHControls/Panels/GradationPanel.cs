using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHControls.Panels
{
    class GradationPanel : Panel
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

            int width = this.Width;
            int height = this.Height ;

            if (width > 0 && height > 0)
            {
                FillGradation(e, width, height);
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

            Rectangle backgroundRectangle = new Rectangle(0, 0, width, height);
            e.Graphics.FillRectangle(linGrBrush, backgroundRectangle);
        }

        private LinearGradientBrush GetGradationBrush()
        {
            LinearGradientBrush brush 
                = new LinearGradientBrush(this.ClientRectangle, _GradationStartColor, _GradationEndColor, _GradationRatio);

            return brush;
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

    }
}
