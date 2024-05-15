﻿using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Embedded_Systems_Project.Addons
{

    /// <summary>
    /// The LEDBulb is a .Net control for Windows Forms that emulates an
    /// LED light with two states On and Off.  The purpose of the control is to 
    /// provide a sleek looking representation of an LED light that is sizable, 
    /// has a transparent background and can be set to different colors.  
    /// </summary>
    public partial class LedBulb : Control
    {

        #region Public and Private Members

        private Color _color;
        private bool _on = true;
        private readonly Color _reflectionColor = Color.FromArgb(180, 255, 255, 255);
        private readonly Color[] _surroundColor = new Color[] { Color.FromArgb(0, 255, 255, 255) };
        private readonly System.Windows.Forms.Timer _timer = new();

        /// <summary>
        /// Gets or Sets the color of the LED light
        /// </summary>
        [DefaultValue(typeof(Color), "153, 255, 54")]
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                DarkColor = ControlPaint.Dark(_color);
                DarkDarkColor = ControlPaint.DarkDark(_color);
                Invalidate();  // Redraw the control
            }
        }

        /// <summary>
        /// Dark shade of the LED color used for gradient
        /// </summary>
        public Color DarkColor { get; protected set; }

        /// <summary>
        /// Very dark shade of the LED color used for gradient
        /// </summary>
        public Color DarkDarkColor { get; protected set; }

        /// <summary>
        /// Gets or Sets whether the light is turned on
        /// </summary>
        public bool On
        {
            get => _on;
            set { _on = value; Invalidate(); }
        }

        #endregion

        #region Constructor

        public LedBulb()
        {
            SetStyle(ControlStyles.DoubleBuffer
            | ControlStyles.AllPaintingInWmPaint
            | ControlStyles.ResizeRedraw
            | ControlStyles.UserPaint
            | ControlStyles.SupportsTransparentBackColor, true);

            Color = Color.FromArgb(255, 153, 255, 54);
            _timer.Tick += new EventHandler(
                (object sender, EventArgs e) => { On = !On; }
            );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Paint event for this UserControl
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Create an offscreen graphics object for double buffering
            Bitmap offScreenBmp = new(ClientRectangle.Width, ClientRectangle.Height);
            using System.Drawing.Graphics g = Graphics.FromImage(offScreenBmp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            // Draw the control
            drawControl(g, On);
            // Draw the image to the screen
            e.Graphics.DrawImageUnscaled(offScreenBmp, 0, 0);
        }

        /// <summary>
        /// Renders the control to an image
        /// </summary>
        private void drawControl(Graphics g, bool on)
        {
            // Is the bulb on or off
            Color lightColor = on ? Color : Color.FromArgb(150, DarkColor);
            Color darkColor = on ? DarkColor : DarkDarkColor;

            // Calculate the dimensions of the bulb
            int width = Width - (Padding.Left + Padding.Right);
            int height = Height - (Padding.Top + Padding.Bottom);
            // Diameter is the lesser of width and height
            int diameter = Math.Min(width, height);
            // Subtract 1 pixel so ellipse doesn't get cut off
            diameter = Math.Max(diameter - 1, 1);

            // Draw the background ellipse
            Rectangle rectangle = new(Padding.Left, Padding.Top, diameter, diameter);
            g.FillEllipse(new SolidBrush(darkColor), rectangle);

            // Draw the glow gradient
            GraphicsPath path = new();
            path.AddEllipse(rectangle);
            PathGradientBrush pathBrush = new(path)
            {
                CenterColor = lightColor,
                SurroundColors = new Color[] { Color.FromArgb(0, lightColor) }
            };
            g.FillEllipse(pathBrush, rectangle);

            // Draw the white reflection gradient
            int offset = Convert.ToInt32(diameter * .15F);
            int diameter1 = Convert.ToInt32(rectangle.Width * .8F);
            Rectangle whiteRect = new(rectangle.X - offset, rectangle.Y - offset, diameter1, diameter1);
            GraphicsPath path1 = new();
            path1.AddEllipse(whiteRect);
            PathGradientBrush pathBrush1 = new(path)
            {
                CenterColor = _reflectionColor,
                SurroundColors = _surroundColor
            };
            g.FillEllipse(pathBrush1, whiteRect);

            // Draw the border
            g.SetClip(ClientRectangle);
            if (On)
            {
                g.DrawEllipse(new Pen(Color.FromArgb(85, Color.Black), 1F), rectangle);
            }
        }

        /// <summary>
        /// Causes the Led to start blinking
        /// </summary>
        /// <param name="milliseconds">Number of milliseconds to blink for. 0 stops blinking</param>
        public void Blink(int milliseconds)
        {
            if (milliseconds > 0)
            {
                On = true;
                _timer.Interval = milliseconds;
                _timer.Enabled = true;
            }
            else
            {
                _timer.Enabled = false;
                On = false;
            }
        }

        #endregion
    }
}
