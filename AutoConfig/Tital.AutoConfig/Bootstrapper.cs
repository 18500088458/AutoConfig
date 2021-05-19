using System;
using System.Reflection;
using Microsoft.Practices.Unity;
using Tital.AutoConfig.Logs;
using Tital.DI;
using Tital.Logs;
using System.Linq;

namespace Tital.AutoConfig
{
    public static class Bootstrapper
    {
        public static readonly IUnityContainer UnityContainer = new UnityContainer();
        private static readonly IDi Di = new UnityDi(UnityContainer);

        public static void Initialise()
        {
            DiUtil.SetDi(Di);
            AutoConfigTypes();
            ExecuteDiConfigs();
            ExecuteInits();
        }

        private static void AutoConfigTypes()
        {
            DiUtil.Di.RegisterType<INow, DefaultNow>(Lifetime.Singleton);
            DiUtil.Di.RegisterType<ILog, Log>(Lifetime.Singleton); 
        }

        private static void ExecuteDiConfigs()
        {
            var type = typeof(IDiConfig);

            foreach (var instance in AllClasses.GetTypes(type).Select(impType => (IDiConfig)Di.Resolve(impType)))
            {
                instance.RegisterTypes(Di);
            }
        }

        private static void ExecuteInits()
        {
            var type = typeof(IBootstrap);
            var types = AllClasses.GetTypes(type).ToArray();
            var bootstrapObjs =
              types
                    .Where(Where)
                    .OrderBy(GetOrder)
                    .Select(impType => (IBootstrap)Di.Resolve(impType))
                    .ToArray();

            foreach (var instance in bootstrapObjs)
            {
                instance.Execute();
            }
        }

        private static bool Where(Type t)
        {
            var attr = t.GetCustomAttribute<BootstrapArribute>();

            return attr == null || !attr.Disable;
        }

        private static int GetOrder(Type t)
        {
            var attr = t.GetCustomAttribute<BootstrapArribute>();
            return attr == null ? 0 : attr.Order;
        }
    }
}
