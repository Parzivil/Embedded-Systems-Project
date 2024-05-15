using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace AquaControls
{
    /// <summary>
    /// Aqua Gauge Control - A Windows User Control.
    /// Author  : Ambalavanar Thirugnanam
    /// Date    : 24th August 2007
    /// email   : ambalavanar.thiru@gmail.com
    /// This is control is for free. You can use for any commercial or non-commercial purposes.
    /// [Please do no remove this header when using this control in your application.]
    /// </summary>
    public partial class AquaGauge : UserControl
    {
        #region Private Attributes
        private float minValue;
        private float maxValue;
        private float threshold;
        private float currentValue;
        private float recommendedValue;
        private int noOfDivisions;
        private int noOfSubDivisions;
        private string dialText;
        private Color dialColor = Color.Lavender;
        private float glossinessAlpha = 25;
        private int oldWidth, oldHeight;
        private readonly int x;
        private readonly int y;
        private int width;
        private int height;
        private readonly float fromAngle = 135F;
        private readonly float toAngle = 405F;
        private bool enableTransparentBackground;
        private bool requiresRedraw;
        private Image backgroundImg;
        private Rectangle rectImg;
        #endregion

        public AquaGauge()
        {
            InitializeComponent();
            x = 5;
            y = 5;
            width = Width - 10;
            height = Height - 10;
            noOfDivisions = 10;
            noOfSubDivisions = 3;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
            Resize += new EventHandler(AquaGauge_Resize);
            requiresRedraw = true;
        }

        #region Public Properties
        /// <summary>
        /// Mininum value on the scale
        /// </summary>
        [DefaultValue(0)]
        [Description("Mininum value on the scale")]
        public float MinValue
        {
            get => minValue;
            set
            {
                if (value < maxValue)
                {
                    minValue = value;
                    if (currentValue < minValue)
                    {
                        currentValue = minValue;
                    }

                    if (recommendedValue < minValue)
                    {
                        recommendedValue = minValue;
                    }

                    requiresRedraw = true;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Maximum value on the scale
        /// </summary>
        [DefaultValue(100)]
        [Description("Maximum value on the scale")]
        public float MaxValue
        {
            get => maxValue;
            set
            {
                if (value > minValue)
                {
                    maxValue = value;
                    if (currentValue > maxValue)
                    {
                        currentValue = maxValue;
                    }

                    if (recommendedValue > maxValue)
                    {
                        recommendedValue = maxValue;
                    }

                    requiresRedraw = true;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or Sets the Threshold area from the Recommended Value. (1-99%)
        /// </summary>
        [DefaultValue(25)]
        [Description("Gets or Sets the Threshold area from the Recommended Value. (1-99%)")]
        public float ThresholdPercent
        {
            get => threshold;
            set
            {
                if (value is > 0 and < 100)
                {
                    threshold = value;
                    requiresRedraw = true;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Threshold value from which green area will be marked.
        /// </summary>
        [DefaultValue(25)]
        [Description("Threshold value from which green area will be marked.")]
        public float RecommendedValue
        {
            get => recommendedValue;
            set
            {
                if (value > minValue && value < maxValue)
                {
                    recommendedValue = value;
                    requiresRedraw = true;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Value where the pointer will point to.
        /// </summary>
        [DefaultValue(0)]
        [Description("Value where the pointer will point to.")]
        public float Value
        {
            get => currentValue;
            set
            {
                if (value >= minValue && value <= maxValue)
                {
                    currentValue = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Background color of the dial
        /// </summary>
        [Description("Background color of the dial")]
        public Color DialColor
        {
            get => dialColor;
            set
            {
                dialColor = value;
                requiresRedraw = true;
                Invalidate();
            }
        }

        /// <summary>
        /// Glossiness strength. Range: 0-100
        /// </summary>
        [DefaultValue(72)]
        [Description("Glossiness strength. Range: 0-100")]
        public float Glossiness
        {
            get => glossinessAlpha * 100 / 220;
            set
            {
                float val = value;
                if (val > 100)
                {
                    value = 100;
                }

                if (val < 0)
                {
                    value = 0;
                }

                glossinessAlpha = value * 220 / 100;
                Refresh();
            }
        }

        /// <summary>
        /// Get or Sets the number of Divisions in the dial scale.
        /// </summary>
        [DefaultValue(10)]
        [Description("Get or Sets the number of Divisions in the dial scale.")]
        public int NoOfDivisions
        {
            get => noOfDivisions;
            set
            {
                if (value is > 1 and < 25)
                {
                    noOfDivisions = value;
                    requiresRedraw = true;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or Sets the number of Sub Divisions in the scale per Division.
        /// </summary>
        [DefaultValue(3)]
        [Description("Gets or Sets the number of Sub Divisions in the scale per Division.")]
        public int NoOfSubDivisions
        {
            get => noOfSubDivisions;
            set
            {
                if (value is > 0 and <= 10)
                {
                    noOfSubDivisions = value;
                    requiresRedraw = true;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or Sets the Text to be displayed in the dial
        /// </summary>
        [Description("Gets or Sets the Text to be displayed in the dial")]
        public string DialText
        {
            get => dialText;
            set
            {
                dialText = value;
                requiresRedraw = true;
                Invalidate();
            }
        }

        /// <summary>
        /// Enables or Disables Transparent Background color.
        /// Note: Enabling this will reduce the performance and may make the control flicker.
        /// </summary>
        [DefaultValue(false)]
        [Description("Enables or Disables Transparent Background color. Note: Enabling this will reduce the performance and may make the control flicker.")]
        public bool EnableTransparentBackground
        {
            get => enableTransparentBackground;
            set
            {
                enableTransparentBackground = value;
                SetStyle(ControlStyles.OptimizedDoubleBuffer, !enableTransparentBackground);
                requiresRedraw = true;
                Refresh();
            }
        }
        #endregion

        #region Overriden Control methods
        /// <summary>
        /// Draws the pointer.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            width = Width - (x * 2);
            height = Height - (y * 2);
            DrawPointer(e.Graphics, (width / 2) + x, (height / 2) + y);
        }

        /// <summary>
        /// Draws the dial background.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!enableTransparentBackground)
            {
                base.OnPaintBackground(e);
            }

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.FillRectangle(new SolidBrush(Color.Transparent), new Rectangle(0, 0, Width, Height));
            if (backgroundImg == null || requiresRedraw)
            {
                backgroundImg = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(backgroundImg);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                width = Width - (x * 2);
                height = Height - (y * 2);
                rectImg = new Rectangle(x, y, width, height);

                //Draw background color
                Brush backGroundBrush = new SolidBrush(Color.FromArgb(120, dialColor));
                if (enableTransparentBackground && Parent != null)
                {
                    _ = width / 60;
                    //g.FillEllipse(new SolidBrush(this.Parent.BackColor), -gg, -gg, this.Width+gg*2, this.Height+gg*2);
                }
                g.FillEllipse(backGroundBrush, x, y, width, height);

                //Draw Rim
                SolidBrush outlineBrush = new(Color.FromArgb(100, Color.SlateGray));
                Pen outline = new(outlineBrush, (float)(width * .03));
                g.DrawEllipse(outline, rectImg);
                Pen darkRim = new(Color.SlateGray);
                g.DrawEllipse(darkRim, x, y, width, height);

                //Draw Callibration
                DrawCalibration(g, rectImg, (width / 2) + x, (height / 2) + y);

                //Draw Colored Rim
                Pen colorPen = new(Color.FromArgb(190, Color.Gainsboro), Width / 40);
                _ = new Pen(Color.FromArgb(250, Color.Black), Width / 200);
                int gap = (int)(Width * 0.03F);
                Rectangle rectg = new(rectImg.X + gap, rectImg.Y + gap, rectImg.Width - (gap * 2), rectImg.Height - (gap * 2));
                g.DrawArc(colorPen, rectg, 135, 270);

                //Draw Threshold
                colorPen = new Pen(Color.FromArgb(200, Color.LawnGreen), Width / 50);
                rectg = new Rectangle(rectImg.X + gap, rectImg.Y + gap, rectImg.Width - (gap * 2), rectImg.Height - (gap * 2));
                float val = MaxValue - MinValue;
                val = 100 * (recommendedValue - MinValue) / val;
                val = (toAngle - fromAngle) * val / 100;
                val += fromAngle;
                float stAngle = val - (270 * threshold / 200);
                if (stAngle <= 135)
                {
                    stAngle = 135;
                }

                float sweepAngle = 270 * threshold / 100;
                if (stAngle + sweepAngle > 405)
                {
                    sweepAngle = 405 - stAngle;
                }

                g.DrawArc(colorPen, rectg, stAngle, sweepAngle);

                //Draw Digital Value
                RectangleF digiRect = new((Width / 2F) - (width / 5F), height / 1.2F, width / 2.5F, Height / 9F);
                RectangleF digiFRect = new((Width / 2) - (width / 7), (int)(height / 1.18), width / 4, Height / 12);
                g.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Gray)), digiRect);
                DisplayNumber(g, currentValue, digiFRect);

                SizeF textSize = g.MeasureString(dialText, Font);
                RectangleF digiFRectText = new((Width / 2) - (textSize.Width / 2), (int)(height / 1.5), textSize.Width, textSize.Height);
                g.DrawString(dialText, Font, new SolidBrush(ForeColor), digiFRectText);
                requiresRedraw = false;
            }
            e.Graphics.DrawImage(backgroundImg, rectImg);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Draws the Pointer.
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        private void DrawPointer(Graphics gr, int cx, int cy)
        {
            float radius = (Width / 2) - (Width * .12F);
            float val = MaxValue - MinValue;

            Image img = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            val = 100 * (currentValue - MinValue) / val;
            val = (toAngle - fromAngle) * val / 100;
            val += fromAngle;

            float angle = GetRadian(val);
            PointF[] pts = new PointF[5];

            pts[0].X = (float)(cx + (radius * Math.Cos(angle)));
            pts[0].Y = (float)(cy + (radius * Math.Sin(angle)));

            pts[4].X = (float)(cx + (radius * Math.Cos(angle - 0.02)));
            pts[4].Y = (float)(cy + (radius * Math.Sin(angle - 0.02)));

            angle = GetRadian(val + 20);
            pts[1].X = (float)(cx + (Width * .09F * Math.Cos(angle)));
            pts[1].Y = (float)(cy + (Width * .09F * Math.Sin(angle)));

            pts[2].X = cx;
            pts[2].Y = cy;

            angle = GetRadian(val - 20);
            pts[3].X = (float)(cx + (Width * .09F * Math.Cos(angle)));
            pts[3].Y = (float)(cy + (Width * .09F * Math.Sin(angle)));

            Brush pointer = new SolidBrush(Color.Black);
            g.FillPolygon(pointer, pts);

            PointF[] shinePts = new PointF[3];
            angle = GetRadian(val);
            shinePts[0].X = (float)(cx + (radius * Math.Cos(angle)));
            shinePts[0].Y = (float)(cy + (radius * Math.Sin(angle)));

            angle = GetRadian(val + 20);
            shinePts[1].X = (float)(cx + (Width * .09F * Math.Cos(angle)));
            shinePts[1].Y = (float)(cy + (Width * .09F * Math.Sin(angle)));

            shinePts[2].X = cx;
            shinePts[2].Y = cy;

            LinearGradientBrush gpointer = new(shinePts[0], shinePts[2], Color.SlateGray, Color.Black);
            g.FillPolygon(gpointer, shinePts);

            Rectangle rect = new(x, y, width, height);
            DrawCenterPoint(g, rect, (width / 2) + x, (height / 2) + y);

            DrawGloss(g);

            gr.DrawImage(img, 0, 0);
        }

        /// <summary>
        /// Draws the glossiness.
        /// </summary>
        /// <param name="g"></param>
        private void DrawGloss(Graphics g)
        {
            RectangleF glossRect = new(
               x + (float)(width * 0.10),
               y + (float)(height * 0.07),
               (float)(width * 0.80),
               (float)(height * 0.7));
            LinearGradientBrush gradientBrush =
                new(glossRect,
                Color.FromArgb((int)glossinessAlpha, Color.White),
                Color.Transparent,
                LinearGradientMode.Vertical);
            g.FillEllipse(gradientBrush, glossRect);

            //TODO: Gradient from bottom
            glossRect = new RectangleF(
               x + (float)(width * 0.25),
               y + (float)(height * 0.77),
               (float)(width * 0.50),
               (float)(height * 0.2));
            int gloss = (int)(glossinessAlpha / 3);
            gradientBrush =
                new LinearGradientBrush(glossRect,
                Color.Transparent, Color.FromArgb(gloss, BackColor),
                LinearGradientMode.Vertical);
            g.FillEllipse(gradientBrush, glossRect);
        }

        /// <summary>
        /// Draws the center point.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        private void DrawCenterPoint(Graphics g, Rectangle rect, int cX, int cY)
        {
            float shift = Width / 5;
            RectangleF rectangle = new(cX - (shift / 2), cY - (shift / 2), shift, shift);
            LinearGradientBrush brush = new(rect, Color.Black, Color.FromArgb(100, dialColor), LinearGradientMode.Vertical);
            g.FillEllipse(brush, rectangle);

            shift = Width / 7;
            rectangle = new RectangleF(cX - (shift / 2), cY - (shift / 2), shift, shift);
            brush = new LinearGradientBrush(rect, Color.SlateGray, Color.Black, LinearGradientMode.ForwardDiagonal);
            g.FillEllipse(brush, rectangle);
        }

        /// <summary>
        /// Draws the Ruler
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        private void DrawCalibration(Graphics g, Rectangle rect, int cX, int cY)
        {
            int noOfParts = noOfDivisions + 1;
            int noOfIntermediates = noOfSubDivisions;
            float currentAngle = GetRadian(fromAngle);
            int gap = (int)(Width * 0.01F);
            float shift = Width / 25;
            Rectangle rectangle = new(rect.Left + gap, rect.Top + gap, rect.Width - gap, rect.Height - gap);

            float x, y, x1, y1, tx, ty, radius;
            radius = (rectangle.Width / 2) - (gap * 5);
            float totalAngle = toAngle - fromAngle;
            float incr = GetRadian(totalAngle / ((noOfParts - 1) * (noOfIntermediates + 1)));

            Pen thickPen = new(Color.Black, Width / 50);
            Pen thinPen = new(Color.Black, Width / 100);
            float rulerValue = MinValue;
            for (int i = 0; i <= noOfParts; i++)
            {
                //Draw Thick Line
                x = (float)(cX + (radius * Math.Cos(currentAngle)));
                y = (float)(cY + (radius * Math.Sin(currentAngle)));
                x1 = (float)(cX + ((radius - (Width / 20)) * Math.Cos(currentAngle)));
                y1 = (float)(cY + ((radius - (Width / 20)) * Math.Sin(currentAngle)));
                g.DrawLine(thickPen, x, y, x1, y1);

                //Draw Strings
                _ = new
                //Draw Strings
                StringFormat();
                tx = (float)(cX + ((radius - (Width / 10)) * Math.Cos(currentAngle)));
                ty = (float)(cY - shift + ((radius - (Width / 10)) * Math.Sin(currentAngle)));
                Brush stringPen = new SolidBrush(ForeColor);
                StringFormat strFormat = new(StringFormatFlags.NoClip)
                {
                    Alignment = StringAlignment.Center
                };
                Font f = new(Font.FontFamily, Width / 23, Font.Style);
                g.DrawString(rulerValue.ToString() + "", f, stringPen, new PointF(tx, ty), strFormat);
                rulerValue += (float)((MaxValue - MinValue) / (noOfParts - 1));
                rulerValue = (float)Math.Round(rulerValue, 2);

                //currentAngle += incr;
                if (i == noOfParts - 1)
                {
                    break;
                }

                for (int j = 0; j <= noOfIntermediates; j++)
                {
                    //Draw thin lines 
                    currentAngle += incr;
                    x = (float)(cX + (radius * Math.Cos(currentAngle)));
                    y = (float)(cY + (radius * Math.Sin(currentAngle)));
                    x1 = (float)(cX + ((radius - (Width / 50)) * Math.Cos(currentAngle)));
                    y1 = (float)(cY + ((radius - (Width / 50)) * Math.Sin(currentAngle)));
                    g.DrawLine(thinPen, x, y, x1, y1);
                }
            }
        }

        /// <summary>
        /// Converts the given degree to radian.
        /// </summary>
        /// <param name="theta"></param>
        /// <returns></returns>
        public float GetRadian(float theta)
        {
            return theta * (float)Math.PI / 180F;
        }

        /// <summary>
        /// Displays the given number in the 7-Segement format.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="number"></param>
        /// <param name="drect"></param>
        private void DisplayNumber(Graphics g, float number, RectangleF drect)
        {
            try
            {
                string num = number.ToString("000.00");
                _ = num.PadLeft(3, '0');
                float shift = 0;
                if (number < 0)
                {
                    shift -= width / 17;
                }
                bool drawDPS = false;
                char[] chars = num.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    char c = chars[i];
                    drawDPS = i < chars.Length - 1 && chars[i + 1] == '.';
                    if (c != '.')
                    {
                        if (c == '-')
                        {
                            DrawDigit(g, -1, new PointF(drect.X + shift, drect.Y), drawDPS, drect.Height);
                        }
                        else
                        {
                            DrawDigit(g, short.Parse(c.ToString()), new PointF(drect.X + shift, drect.Y), drawDPS, drect.Height);
                        }
                        shift += 15 * width / 250;
                    }
                    else
                    {
                        shift += 2 * width / 250;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Draws a digit in 7-Segement format.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="number"></param>
        /// <param name="position"></param>
        /// <param name="dp"></param>
        /// <param name="height"></param>
        private void DrawDigit(Graphics g, int number, PointF position, bool dp, float height)
        {
            float width;
            width = 10F * height / 13;

            Pen outline = new(Color.FromArgb(40, dialColor));
            Pen fillPen = new(Color.Black);

            #region Form Polygon Points
            //Segment A
            PointF[] segmentA = new PointF[5];
            segmentA[0] = segmentA[4] = new PointF(position.X + GetX(2.8F, width), position.Y + GetY(1F, height));
            segmentA[1] = new PointF(position.X + GetX(10, width), position.Y + GetY(1F, height));
            segmentA[2] = new PointF(position.X + GetX(8.8F, width), position.Y + GetY(2F, height));
            segmentA[3] = new PointF(position.X + GetX(3.8F, width), position.Y + GetY(2F, height));

            //Segment B
            PointF[] segmentB = new PointF[5];
            segmentB[0] = segmentB[4] = new PointF(position.X + GetX(10, width), position.Y + GetY(1.4F, height));
            segmentB[1] = new PointF(position.X + GetX(9.3F, width), position.Y + GetY(6.8F, height));
            segmentB[2] = new PointF(position.X + GetX(8.4F, width), position.Y + GetY(6.4F, height));
            segmentB[3] = new PointF(position.X + GetX(9F, width), position.Y + GetY(2.2F, height));

            //Segment C
            PointF[] segmentC = new PointF[5];
            segmentC[0] = segmentC[4] = new PointF(position.X + GetX(9.2F, width), position.Y + GetY(7.2F, height));
            segmentC[1] = new PointF(position.X + GetX(8.7F, width), position.Y + GetY(12.7F, height));
            segmentC[2] = new PointF(position.X + GetX(7.6F, width), position.Y + GetY(11.9F, height));
            segmentC[3] = new PointF(position.X + GetX(8.2F, width), position.Y + GetY(7.7F, height));

            //Segment D
            PointF[] segmentD = new PointF[5];
            segmentD[0] = segmentD[4] = new PointF(position.X + GetX(7.4F, width), position.Y + GetY(12.1F, height));
            segmentD[1] = new PointF(position.X + GetX(8.4F, width), position.Y + GetY(13F, height));
            segmentD[2] = new PointF(position.X + GetX(1.3F, width), position.Y + GetY(13F, height));
            segmentD[3] = new PointF(position.X + GetX(2.2F, width), position.Y + GetY(12.1F, height));

            //Segment E
            PointF[] segmentE = new PointF[5];
            segmentE[0] = segmentE[4] = new PointF(position.X + GetX(2.2F, width), position.Y + GetY(11.8F, height));
            segmentE[1] = new PointF(position.X + GetX(1F, width), position.Y + GetY(12.7F, height));
            segmentE[2] = new PointF(position.X + GetX(1.7F, width), position.Y + GetY(7.2F, height));
            segmentE[3] = new PointF(position.X + GetX(2.8F, width), position.Y + GetY(7.7F, height));

            //Segment F
            PointF[] segmentF = new PointF[5];
            segmentF[0] = segmentF[4] = new PointF(position.X + GetX(3F, width), position.Y + GetY(6.4F, height));
            segmentF[1] = new PointF(position.X + GetX(1.8F, width), position.Y + GetY(6.8F, height));
            segmentF[2] = new PointF(position.X + GetX(2.6F, width), position.Y + GetY(1.3F, height));
            segmentF[3] = new PointF(position.X + GetX(3.6F, width), position.Y + GetY(2.2F, height));

            //Segment G
            PointF[] segmentG = new PointF[7];
            segmentG[0] = segmentG[6] = new PointF(position.X + GetX(2F, width), position.Y + GetY(7F, height));
            segmentG[1] = new PointF(position.X + GetX(3.1F, width), position.Y + GetY(6.5F, height));
            segmentG[2] = new PointF(position.X + GetX(8.3F, width), position.Y + GetY(6.5F, height));
            segmentG[3] = new PointF(position.X + GetX(9F, width), position.Y + GetY(7F, height));
            segmentG[4] = new PointF(position.X + GetX(8.2F, width), position.Y + GetY(7.5F, height));
            segmentG[5] = new PointF(position.X + GetX(2.9F, width), position.Y + GetY(7.5F, height));

            //Segment DP
            #endregion

            #region Draw Segments Outline
            g.FillPolygon(outline.Brush, segmentA);
            g.FillPolygon(outline.Brush, segmentB);
            g.FillPolygon(outline.Brush, segmentC);
            g.FillPolygon(outline.Brush, segmentD);
            g.FillPolygon(outline.Brush, segmentE);
            g.FillPolygon(outline.Brush, segmentF);
            g.FillPolygon(outline.Brush, segmentG);
            #endregion

            #region Fill Segments
            //Fill SegmentA
            if (IsNumberAvailable(number, 0, 2, 3, 5, 6, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentA);
            }

            //Fill SegmentB
            if (IsNumberAvailable(number, 0, 1, 2, 3, 4, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentB);
            }

            //Fill SegmentC
            if (IsNumberAvailable(number, 0, 1, 3, 4, 5, 6, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentC);
            }

            //Fill SegmentD
            if (IsNumberAvailable(number, 0, 2, 3, 5, 6, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentD);
            }

            //Fill SegmentE
            if (IsNumberAvailable(number, 0, 2, 6, 8))
            {
                g.FillPolygon(fillPen.Brush, segmentE);
            }

            //Fill SegmentF
            if (IsNumberAvailable(number, 0, 4, 5, 6, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentF);
            }

            //Fill SegmentG
            if (IsNumberAvailable(number, 2, 3, 4, 5, 6, 8, 9, -1))
            {
                g.FillPolygon(fillPen.Brush, segmentG);
            }
            #endregion

            //Draw decimal point
            if (dp)
            {
                g.FillEllipse(fillPen.Brush, new RectangleF(
                    position.X + GetX(10F, width),
                    position.Y + GetY(12F, height),
                    width / 7,
                    width / 7));
            }
        }

        /// <summary>
        /// Gets Relative X for the given width to draw digit
        /// </summary>
        /// <param name="x"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private float GetX(float x, float width)
        {
            return x * width / 12;
        }

        /// <summary>
        /// Gets relative Y for the given height to draw digit
        /// </summary>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private float GetY(float y, float height)
        {
            return y * height / 15;
        }

        /// <summary>
        /// Returns true if a given number is available in the given list.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="listOfNumbers"></param>
        /// <returns></returns>
        private bool IsNumberAvailable(int number, params int[] listOfNumbers)
        {
            if (listOfNumbers.Length > 0)
            {
                foreach (int i in listOfNumbers)
                {
                    if (i == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Restricts the size to make sure the height and width are always same.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AquaGauge_Resize(object sender, EventArgs e)
        {
            if (Width < 136)
            {
                Width = 136;
            }
            if (oldWidth != Width)
            {
                Height = Width;
                oldHeight = Width;
            }
            if (oldHeight != Height)
            {
                Width = Height;
                oldWidth = Width;
            }
        }
        #endregion
    }
}
