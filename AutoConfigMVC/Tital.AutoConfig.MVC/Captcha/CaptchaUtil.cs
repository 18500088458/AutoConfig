using System;
using System.Collections.Concurrent;
using System.Web;

namespace Tital.AutoConfig.MVC.Captcha
{
    public static class CaptchaUtil
    {
        private static readonly ConcurrentDictionary<string, CaptchaConfig> CaptchaConfigs =
            new ConcurrentDictionary<string, CaptchaConfig>();

        /// <summary>
        /// 验证码输入验证
        /// </summary>
        /// <param name="id">验证ID,验证码Image的URL中的ID</param>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static CaptchaValidateResult Validate(string id, string input)
        {
            CaptchaConfig config = GetCachedConfig(id);
            //1.缓存中不存在此验证码ID对应的配置信息
            if (config == null)
            {
                return CaptchaValidateResult.ConfigNotExist;
            }

            CaptchaData captcha = GetCachedCaptcha(id);
            //2.Session已过期或不存在
            if (captcha == null)
            {
                return CaptchaValidateResult.Timeout;
            }

            //3.验证太频繁
            if (captcha.RenderedAt.AddSeconds(config.MinInterval) > DateTime.Now)
            {
                RemoveCachedCaptcha(id);
                return CaptchaValidateResult.TooQuickly;
            }
            //4.输入不匹配
            if (string.Compare(input, captcha.Code, config.IgnoreCase) != 0)
            {
                RemoveCachedCaptcha(id);
                return CaptchaValidateResult.NotMatch;
            }
            //5.正确
            RemoveCachedCaptcha(id);
            return CaptchaValidateResult.Success;
        }

        //获取验证码配置信息
        internal static CaptchaConfig GetCachedConfig(string id)
        {
            return CaptchaConfigs[id];
        }

        /// <summary>
        /// 注册验证码配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="config"></param>
        public static void SetCachedConfig(string id, CaptchaConfig config)
        {
            if (!CaptchaConfigs.ContainsKey(id))
                CaptchaConfigs.TryAdd(id, config);
        }

        //Session中存储CaptchaData
        internal static void SetCachedCaptcha(string id, CaptchaData captcha)
        {
            HttpContext.Current.Session[id] = captcha;
        }

        //从Session中获取CaptchaData
        private static CaptchaData GetCachedCaptcha(string id)
        {
            return HttpContext.Current.Session[id] as CaptchaData;
        }

        //Session中移除CaptchaData
        private static void RemoveCachedCaptcha(string id)
        {
            HttpContext.Current.Session.Remove(id);
        }
    }
}
