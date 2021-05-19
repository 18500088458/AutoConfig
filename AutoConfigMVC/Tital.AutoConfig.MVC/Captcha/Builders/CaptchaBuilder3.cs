using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Tital.Drawing.NGif;

namespace Tital.AutoConfig.MVC.Captcha.Builders
{
    internal class CaptchaBuilder3 : CaptchaBuilder
    {
        public bool FontTextRenderingHint = false;
        public override System.Drawing.Image Build(string code)
        {
            Bitmap frame;
            MemoryStream data = new MemoryStream();
            var gif = new AnimatedGifEncoder();
            gif.Start(data);
            gif.SetDelay(10);
            gif.SetRepeat(0);
            for (int i = 0; i < 20; i++)
            {
                CreateImageBmp(code, out frame);
                DisposeImageBmp(ref frame);
                gif.AddFrame(frame);
            }
            gif.Finish(); 
            return Image.FromStream(data);
        }
        //生成一帧的BMP图象
        private void CreateImageBmp(string code, out Bitmap ImageFrame)
        {
            //获得验证码字符
            char[] CodeCharArray = code.ToCharArray();
            //图像的宽度-与验证码的长度成一定比例
            float fontSize = (float)((Width - 4) / CodeCharArray.Length / 1.3);
            //创建一个长20，宽iwidth的图像对象
            ImageFrame = new Bitmap(Width, Height);
            //创建一个新绘图对象
            Graphics ImageGraphics = Graphics.FromImage(ImageFrame);
            //清除背景色，并填充背景色
            //Note:Color.Transparent为透明
            ImageGraphics.Clear(Color.White);
            //绘图用的字体和字号
            Font CodeFont = new Font("宋体", fontSize, FontStyle.Bold);
            //绘图用的刷子大小
            Brush ImageBrush = new SolidBrush(Color.Red);
            //字体高度计算
            int FontHeight = (int)Math.Max(Height - fontSize - 3, 2);
            //创建随机对象
            Random rand = new Random();
            //开始随机安排字符的位置，并画到图像里
            for (int i = 0; i < CodeCharArray.Length; i++)
            {
                //生成随机点，决定字符串的开始输出范围
                int[] FontCoordinate = new int[2];
                FontCoordinate[0] = (int)(i * fontSize + rand.Next(1)) + 3;
                FontCoordinate[1] = rand.Next(FontHeight);
                Point FontDrawPoint = new Point(FontCoordinate[0], FontCoordinate[1]);
                //消除锯齿操作
                if (FontTextRenderingHint)
                    ImageGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
                else
                    ImageGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //格式化刷子属性-用指定的刷子、颜色等在指定的范围内画图
                ImageGraphics.DrawString(CodeCharArray[i].ToString(), CodeFont, ImageBrush, FontDrawPoint);
            }
            ImageGraphics.Dispose();
        }
        //处理生成的BMP
        private void DisposeImageBmp(ref Bitmap ImageFrame)
        {
            //创建绘图对象
            Graphics ImageGraphics = Graphics.FromImage(ImageFrame);
            //创建铅笔对象
            Pen ImagePen = new Pen(Color.Red, 1);
            //创建随机对象
            Random rand = new Random();
            //创建随机点
            Point[] RandPoint = new Point[2];
            //随机画线
            for (int i = 0; i < 15; i++)
            {
                RandPoint[0] = new Point(rand.Next(ImageFrame.Width), rand.Next(ImageFrame.Height));
                RandPoint[1] = new Point(rand.Next(ImageFrame.Width), rand.Next(ImageFrame.Height));
                ImageGraphics.DrawLine(ImagePen, RandPoint[0], RandPoint[1]);
            }
            ImageGraphics.Dispose();
        }
    }
}
