using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Tital.DI;

namespace Tital.AutoConfig.MVC
{
    public class MvcBootstrap : IBootstrap
    {
        public void Execute()
        {
            var oldFilterAttributeFilterProvider = FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().FirstOrDefault();

            if (oldFilterAttributeFilterProvider != null)
                FilterProviders.Providers.Remove(oldFilterAttributeFilterProvider);
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(Bootstrapper.UnityContainer));

            var oldHandleErrorAttribute = GlobalFilters.Filters.OfType<HandleErrorAttribute>().FirstOrDefault();

            if (oldHandleErrorAttribute != null)
                GlobalFilters.Filters.Remove(oldHandleErrorAttribute);
            GlobalFilters.Filters.Add(new MvcHandleErrorAttribute());

            DependencyResolver.SetResolver(new UnityDependencyResolver(Bootstrapper.UnityContainer));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(Bootstrapper.UnityContainer);
            GlobalConfiguration.Configuration.Filters.Add(new WebApiErrorAttribute());
            DynamicModuleUtility.RegisterModule(typeof(UnhandledExceptionModule));
        }
    }
}
