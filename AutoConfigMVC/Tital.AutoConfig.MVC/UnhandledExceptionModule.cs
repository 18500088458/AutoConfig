using System;
using System.Web;
using Tital.DI;
using Tital.Logs;

namespace Tital.AutoConfig.MVC
{
    /// <summary>
    /// 未处理异常类 HttpModule
    /// 用于处理程序中未处理的异常
    /// </summary>
    public class UnhandledExceptionModule : IHttpModule
    {
        static readonly object InitLock = new object();
        private bool _initialized;

        public void Init(HttpApplication app)
        {
            if (_initialized)
                return;

            lock (InitLock)
            {
                if (_initialized)
                    return;
                app.Error += app_Error;
                _initialized = true;
            }
        }

        private static void app_Error(object sender, EventArgs e)
        {
            DoException(((HttpApplication)sender).Server.GetLastError());
        }

        private static void DoException(Exception exp)
        {
            if (exp == null) return;
            exp = exp.GetFirst() ?? exp;
            DiUtil.Di.Resolve<ILog>().Error(exp, "WEB未处理异常");
        }

        public void Dispose()
        {

        }
    }
}
