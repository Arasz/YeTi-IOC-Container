using NUnit.Framework;
using Shouldly;

namespace YeTi.Tests
{
    public interface ITestInterface
    {
    }

    /// <summary>
    /// YeTi IOC container test <remarks> Tests naming convention
    /// [UnitOfWork_StateUnderTest_ExpectedBehavior] </remarks>
    /// </summary>
    [TestFixture]
    public class YeTiContainerTest
    {
        [Test]
        public void ResolvesRegisteredComponent_RegisteredTypeAsGenericParameter_CreatedObjectOfGivenType()
        {
            var container = new YeTiContainer();
            container.Register<ITestInterface, TestInterfaceImplementation>();

            var resolvedObject = container.Resolve<ITestInterface>();

            resolvedObject.ShouldBeOfType<TestInterfaceImplementation>();
        }

        [Test]
        public void
            ResolvesRegisteredComponentsWithParameters_RegisteredTypeAsGenericParameterAndParameters_CreatedObjectOfGivenType
            ()
        {
            var container = new YeTiContainer();
            container.Register<ITestInterface, TestImplementationWithDependency>();

            var resolvedObject = container.Resolve<TestImplementationWithDependency>();

            resolvedObject.ShouldBeOfType<TestImplementationWithDependency>();
        }
    }

    /// <summary>
    /// Any dependency 
    /// </summary>
    internal class Dependency
    {
    }

    /// <summary>
    /// Class which takes dependency in constructor 
    /// </summary>
    internal class TestImplementationWithDependency : ITestInterface
    {
        public TestImplementationWithDependency(Dependency dependency)
        {
        }
    }

    internal class TestInterfaceImplementation : ITestInterface
    {
    }
}