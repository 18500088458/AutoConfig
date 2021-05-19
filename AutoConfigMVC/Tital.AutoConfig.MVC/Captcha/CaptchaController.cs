using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace Tital.AutoConfig.MVC.Captcha
{
    public class CaptchaController : Controller
    {
        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return HttpNotFound();
            var config = CaptchaUtil.GetCachedConfig(id);

            if (config == null) return HttpNotFound();
            var code = RandomString.Next(config.CharsScope, config.CaptchaLength);
            CaptchaUtil.SetCachedCaptcha(id, new CaptchaData(code));
            var img = config.Builder.Build(code);

            var ms = new MemoryStream();
            img.Save(ms, ImageFormat.Gif);
            ms.Position = 0;
            return new FileStreamResult(ms, "image/png");
        }


        [HttpPost]
        public ActionResult Valid(string id, string input)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(input)) return HttpNotFound();

            return Content(CaptchaUtil.Validate(id, input).ToString("d"));
        }
    }
}
