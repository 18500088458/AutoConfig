using System;
using System.Drawing;

namespace Tital.AutoConfig.MVC.Captcha.Builders
{
    internal class CaptchaBuilder2 : CaptchaBuilder
    {
        public static Brush[] bs = { Brushes.Black, Brushes.Red, Brushes.DarkBlue, Brushes.Blue, Brushes.Orange, Brushes.Brown, Brushes.DarkCyan, Brushes.Purple };
        public override System.Drawing.Image Build(string code)
        {
            return CreateValidateImage(code);
        }

        public static Bitmap CreateValidateImage(string code)
        {
            //定义字体颜色
            Color[] srColors = { Color.Black, Color.Red, Color.DarkBlue, Color.Blue, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //定义图片弯曲的角度
            int srseedangle = 45;
            //定义图象 
            Bitmap srBmp = new Bitmap(code.Length * 20, 30);
            //画图
            Graphics srGraph = Graphics.FromImage(srBmp);
            //清空图象
            srGraph.Clear(Color.AliceBlue);
            //给图象画边框
            srGraph.DrawRectangle(new Pen(Color.Black, 0), 0, 0, srBmp.Width - 1, srBmp.Height - 1);
            //定义随即数
            Random srRandom = new Random(DateTime.Now.Millisecond);
            //定义画笔
            Pen srPen = new Pen(Color.LightGray, 0);
            //画噪点
            for (int i = 0; i < 300; i++)
            {
                srGraph.DrawRectangle(srPen, srRandom.Next(1, srBmp.Width - 2), srRandom.Next(1, srBmp.Height - 2), 1, 1);
            }
            //将字符串转化为字符数组 
            char[] srchars = code.ToCharArray();
            //封状文本
            StringFormat srFormat = new StringFormat(StringFormatFlags.NoClip);
            //设置文本垂直居中
            srFormat.Alignment = StringAlignment.Center;
            //设置文本水平居中
            srFormat.LineAlignment = StringAlignment.Center;
            //定义字体
            string[] srFonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            //循环画出每个字符
            for (int i = 0, j = srchars.Length; i < j; i++)
            {
                //定义字体 参数分别为字体样式 字体大小 字体字形
                Font srFont = new Font(srFonts[srRandom.Next(5)], srRandom.Next(10, 25), FontStyle.Regular);
                //填充图形
                Brush srBrush = new SolidBrush(srColors[srRandom.Next(7)]);
                //定义坐标
                Point srPoint = new Point(16, 16);
                //定义倾斜角度
                float srangle = srRandom.Next(-srseedangle, srseedangle);
                //倾斜
                srGraph.TranslateTransform(srPoint.X, srPoint.Y);
                srGraph.RotateTransform(srangle);
                //填充字符
                srGraph.DrawString(srchars[i].ToString(), srFont, srBrush, 1, 1, srFormat);
                //回归正常
                srGraph.RotateTransform(-srangle);
                srGraph.TranslateTransform(2, -srPoint.Y);
            }
            srGraph.Dispose();
            return srBmp;
        }
    }
}
