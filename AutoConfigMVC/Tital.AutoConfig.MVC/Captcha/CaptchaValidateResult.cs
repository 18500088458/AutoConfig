using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tital.AutoConfig.MVC.Captcha
{
    /// <summary>
    /// 验证码错误类别
    /// </summary>
    public enum CaptchaValidateResult
    {
        /// <summary>
        /// 验证成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 验证码已过期
        /// </summary>
        Timeout = 1,

        /// <summary>
        /// 验证太频繁
        /// </summary>
        TooQuickly = 2,

        /// <summary>
        /// 验证码不匹配
        /// </summary>
        NotMatch = 3,

        /// <summary>
        /// Application中不存在此ID对应的验证码配置信息
        /// </summary>
        ConfigNotExist = 4
    }
}
