using System;
using Tital.DI; 
using Tital.Logs;

namespace Tital.AutoConfig
{
    public class ExceptionBootstrap : IBootstrap
    {
        public void Execute()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        static void OnUnhandledException(object o, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;
            var baseException = exception.GetBaseException();

            DoException(baseException);
        }

        private static void DoException(Exception exp)
        {
            if (exp == null) return;
            exp = exp.GetFirst() ?? exp;
            DiUtil.Di.Resolve<ILog>().Error(exp, "AppDomain未处理异常。");
        }
    }
}
