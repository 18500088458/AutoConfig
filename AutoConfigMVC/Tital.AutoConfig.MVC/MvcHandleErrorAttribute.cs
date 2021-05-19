using System.Web.Mvc;
using Tital.DI;
using Tital.Logs;

namespace Tital.AutoConfig.MVC
{
    public class MvcHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            DiUtil.Di.Resolve<ILog>().Error(filterContext.Exception, "全局异常");
        }
    }
}
