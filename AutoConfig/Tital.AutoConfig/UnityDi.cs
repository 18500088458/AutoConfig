using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Tital.DI;

namespace Tital.AutoConfig
{
    internal class UnityDi : IDi
    {
        private readonly IUnityContainer _container;

        public UnityDi(IUnityContainer container)
        {
            this._container = container;
        }

        public IDi RegisterInstance(Type t, string name, object instance)
        {
            _container.RegisterInstance(t, name, instance);
            return this;
        }

        public IDi RegisterType(Type from, Type to, string name, Lifetime lifeTime = Lifetime.PerResolve)
        {
            LifetimeManager lifetimeManager;

            switch (lifeTime)
            {
                case Lifetime.PerResolve:
                    lifetimeManager = new PerThreadLifetimeManager();
                    break;                
                case Lifetime.Singleton:
                    lifetimeManager = new ContainerControlledLifetimeManager();
                    break;
                default:
                    throw new NotImplementedException();
            }
            _container.RegisterType(from, to, name, lifetimeManager);
            return this;
        }

        public object Resolve(Type t, string name)
        {
            try
            {
                return _container.Resolve(t, name);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            return _container.ResolveAll(t);
        }
    }
}
