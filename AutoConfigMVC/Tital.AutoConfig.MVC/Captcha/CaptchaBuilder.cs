using Tital.AutoConfig.MVC.Captcha.Builders;

namespace Tital.AutoConfig.MVC.Captcha
{
    /// <summary>
    /// Captcha图片构建基类
    /// </summary>
    public abstract class CaptchaBuilder
    {
        /// <summary>
        /// 验证码的宽
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 验证码的高
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 构建验证码的虚方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public abstract System.Drawing.Image Build(string code);

        /// <summary>
        /// 通过指定样式创建Captcha图片构建基类
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static CaptchaBuilder Create(CaptchaImageStyles style)
        {
            CaptchaBuilder ret = null;
            switch (style)
            {
                case CaptchaImageStyles.Style1:
                    ret = new CaptchaBuilder1();
                    break;
                case CaptchaImageStyles.Style2:
                    ret = new CaptchaBuilder2();
                    break;
                case CaptchaImageStyles.Style3:
                    ret = new CaptchaBuilder3();
                    break;
                case CaptchaImageStyles.Style4:
                    ret = new CaptchaBuilder4();
                    break;
            }
            return ret;
        }
    }

    /// <summary>
    /// 验证码样式
    /// </summary>
    public enum CaptchaImageStyles
    {
        /// <summary>
        /// 样式1
        /// </summary>
        Style1,
        /// <summary>
        /// 样式2
        /// </summary>
        Style2,
        /// <summary>
        /// 样式3
        /// </summary>
        Style3,
        /// <summary>
        /// 样式4
        /// </summary>
        Style4,
    }
}
