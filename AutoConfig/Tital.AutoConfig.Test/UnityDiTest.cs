using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tital.DI;

namespace Tital.AutoConfig.Test
{
    [TestClass]
    public class UnityDiTest
    {
        [TestMethod]
        public void TestRegisterType()
        {
            DiUtil.Di.RegisterType<IAnimal, Donkey>();
            Assert.AreNotSame(DiUtil.Di.Resolve<IAnimal>() ,DiUtil.Di.Resolve<IAnimal>() ); 
        }

        [TestMethod]
        public void TestRegisterType_Singleton()
        {
            DiUtil.Di.RegisterType<IAnimal, Donkey>(Lifetime.Singleton);
            Assert.AreSame(DiUtil.Di.Resolve<IAnimal>(), DiUtil.Di.Resolve<IAnimal>());
        }

        private interface IAnimal
        {
        }

        private class Donkey : IAnimal
        {
        }
    }
}
