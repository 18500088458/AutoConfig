using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Tital.AutoConfig.MVC.Captcha.Builders
{
    internal class CaptchaBuilder1 : CaptchaBuilder
    {
        public override Image Build(string code)
        {
            var hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.LightGray, Color.DarkGray);
            return CaptchaImageMaker.GetImage(code, Width, Height, hatchBrush);
        }
    }

    internal class CaptchaImageMaker
    {
        private Bitmap _image { get; set; }
        private HatchBrush _hatchBrush { get; set; }

        private float _currX { get; set; }
        private int _width { get; set; }
        private int _height { get; set; }

        private FontFamily _fontFamily { get; set; }

        private float _fontSize { get; set; }

        private string _code { get; set; }

        internal static Bitmap GetImage(string code, int width, int height, HatchBrush hatchBrush)
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentException("code can not be null or empty.");
            var cim = new CaptchaImageMaker(code, width, height, hatchBrush);
            return cim.GetImage();
        }

        private CaptchaImageMaker(string code, int width, int height, HatchBrush hatchBrush)
        {
            this._width = width;
            this._height = height;
            _code = code;
            _fontFamily = FontFamily.GenericSerif;
            _fontSize = 10f;
            _hatchBrush = hatchBrush;
            _image = new Bitmap(this._width, this._height, PixelFormat.Format32bppArgb);
        }

        private Bitmap GetImage()
        {
            var g = Graphics.FromImage(_image);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = GetFrame(g);

            foreach (var c in _code)
            {
                DrawText(g, c.ToString());
            }

            AddNoise(rect, g);
            g.Save();
            return _image;
        }

        private void DrawText(Graphics g, string text)
        {

            _fontSize = (float)this._height / 2;
            var font = new Font(this._fontFamily, _fontSize, FontStyle.Bold);
            var size = g.MeasureString(text, font);
            var bitmap = GetCharBitMap(text, _image.Height, g);
            var gr = Graphics.FromImage(bitmap);
            DistortImage(gr);
            gr.DrawString(text, font, _hatchBrush, 0, 0);
            g.DrawImage(bitmap, _currX, 0);
            _currX += size.Width;
        }

        private Rectangle GetFrame(Graphics g)
        {
            var rect = new Rectangle(0, 0, (int)this._width, (int)this._height);
            var brush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
            g.FillRectangle(brush, rect);
            return rect;
        }

        private Bitmap GetCharBitMap(string text, float maxHeight, Graphics g)
        {
            var fontSize = maxHeight;
            var font = new Font(this._fontFamily, fontSize, FontStyle.Bold);
            var size = g.MeasureString(text, font);
            while (size.Height > maxHeight)
            {
                fontSize--;
                font = new Font(this._fontFamily, fontSize, FontStyle.Bold);
                size = g.MeasureString(text, font);
            }

            return new Bitmap((int)size.Width, (int)size.Height, PixelFormat.Format32bppArgb);
        }

        private void DistortImage(Graphics g)
        {
            Matrix tran = new Matrix();
            var method = new Random().Next(3);

            switch (method)
            {
                case 0:
                    tran.Rotate(15);
                    break;
                case 1:
                    tran.Shear(0.2f, 0.3f);
                    break;
                case 2:
                    tran.Scale(1.3f, 1, MatrixOrder.Append);
                    break;
            }

            g.MultiplyTransform(tran);
        }

        private void AddNoise(Rectangle rect, Graphics g)
        {
            for (var i = 0; i < (int)(rect.Width * rect.Height / 50F); i++)
            {
                var x = new Random().Next(rect.Width);
                var y = new Random().Next(rect.Height);
                g.DrawString("*", new Font(_fontFamily, 2, FontStyle.Strikeout), _hatchBrush, x, y);
            }
        }
    }
}
