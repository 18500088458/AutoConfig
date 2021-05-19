using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tital.AutoConfig.Test
{
    [TestClass]
    public class AssemblyInitializer
    {
        [TestMethod]
        [AssemblyInitialize]
        public static void RegisterTypes(TestContext context)
        {
            Bootstrapper.Initialise();
        }
    }
}