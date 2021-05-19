using System;

namespace Tital.AutoConfig.MVC.Captcha
{
    /// <summary>
    /// 验证码设置信息存储在Application中
    /// </summary>
    [Serializable]
    public class CaptchaConfig
    {
        /// <summary>
        /// 验证码范围
        /// </summary>
        public char[] CharsScope { get; set; }
        /// <summary>
        /// 验证码长度
        /// </summary>
        public int CaptchaLength { get; set; }

        /// <summary>
        /// 验证码提交最小间隔，0表示不限制，单位秒
        /// </summary>
        public int MinInterval { get; set; }

        /// <summary>
        /// 是否区分大小写 true:忽略大小写
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// 验证码构建器
        /// </summary>
        public CaptchaBuilder Builder { get; set; }
    }

    /// <summary>
    /// 验证码数据存储在Session中
    /// </summary>
    [Serializable]
    internal class CaptchaData
    {
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime RenderedAt { get; set; }

        /// <summary>
        /// 验证码值
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        public CaptchaData(string code)
        {
            Code = code;
            RenderedAt = DateTime.Now;
        }
    }

}
