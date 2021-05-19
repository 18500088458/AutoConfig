using System.Web.Http.Filters;
using Tital.DI;
using Tital.Logs;

namespace Tital.AutoConfig.MVC
{
    public class WebApiErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            DiUtil.Di.Resolve<ILog>().Error(actionExecutedContext.Exception, "WebApi异常");
        }
    }
}
