using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Concurrent;

namespace Tital.AutoConfig.MVC.Captcha
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="refreshControlId">获取或设置验证码关联的刷新控件ID，可为空</param>
        /// <param name="height">高度</param>
        /// <param name="length">验证码长度</param>
        /// <param name="charsScope">验证码范围</param>
        /// <param name="style">验证码样式</param>
        /// <param name="minInterval">验证码提交最小间隔，0表示不限制，单位秒</param>
        /// <param name="ignoreCase">是否区分大小写 true:忽略大小写</param>
        /// <param name="refreshInterval">自动刷新验证码间隔时间（分钟），0表示不刷新</param>
        /// <param name="id">编码</param>
        /// <param name="inputControlId">验证码关联的输入控件ID</param>
        /// <param name="width">宽度</param>
        /// <returns></returns>
        public static MvcHtmlString Captcha(this HtmlHelper htmlHelper,
            string id,
            string inputControlId,
            string refreshControlId,
            int width = 131,
            int height = 34,
            int length = 5,
            string charsScope = "ACDEFGHJKLNPQRTUVXYZ2346789",
            CaptchaImageStyles style = CaptchaImageStyles.Style4,
            int minInterval = 5,
            bool ignoreCase = true,
            int refreshInterval = 0)
        {
            var captchaId = StringUtil.ToBase64String(string.Format("{0}|{1}", id, HttpContext.Current.Request.Url));
            var builder = CaptchaBuilder.Create(style);

            builder.Width = width;
            builder.Height = height;
            CaptchaUtil.SetCachedConfig(captchaId,
                                        new CaptchaConfig
                                            {
                                                CaptchaLength = length,
                                                IgnoreCase = ignoreCase,
                                                MinInterval = minInterval,
                                                CharsScope = charsScope.ToArray(),
                                                Builder = builder,
                                            });



            var sb = new StringBuilder();

            sb.AppendFormat("<img id='{0}' width='{1}px' height='{2}px'/>", id, width, height);
            sb.AppendFormat(
                "<script>$(function () {{var {0}=new Captcha('{3}','{0}','{1}','{2}');{0}.flash();{0}.regClick({0});$('#{0}').click(function(){{a.flash();}});}});</script>",
                id,
                inputControlId,
                refreshControlId,
                captchaId);

            if (refreshInterval != 0)
                sb.AppendFormat("setInterval('{0}.flash()', {1});", id, refreshInterval * 60000);
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
