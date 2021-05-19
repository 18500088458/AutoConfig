using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tital.Drawing.NGif;

namespace Tital.AutoConfig.MVC.Captcha.Builders
{
    class CaptchaBuilder4 : CaptchaBuilder
    {
        public override Image Build(string code)
        {
            Bitmap frame;
            MemoryStream data = new MemoryStream();
            var gif = new AnimatedGifEncoder();
            gif.Start(data);
            gif.SetDelay(300);
            gif.SetRepeat(0);
            for (int i = 0; i < 20; i++)
            {
                gif.AddFrame(CaptchaBuilder2.CreateValidateImage(code));
            }
            gif.Finish();
            return Image.FromStream(data);
        }
    }
}
