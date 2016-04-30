using NUnit.Framework;
using Shouldly;
using YeTi.Exceptions;

namespace YeTi.Tests
{
    /// <summary>
    /// Interface used for tests 
    /// </summary>
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
        public void ResolvesComponentWithMoreThenOneConstructor_RegisteredTypeAsGenericParameters_Throws()
        {
            var container = new YeTiContainer();

            container.Register<ITestInterface, TestInterfaceImplementationWithMultipleConsturcutors>();
            container.Register<Dependency, Dependency>();

            var exception = Assert.Throws<ComponentHasMultipleConstructorsException>(() => container.Resolve<ITestInterface>());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ComponentHasMultipleConstructorsException>();
        }

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
            container.Register<Dependency, Dependency>();

            var resolvedObject = container.Resolve<ITestInterface>();

            resolvedObject.ShouldBeOfType<TestImplementationWithDependency>();
        }
    }

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

    internal class TestInterfaceImplementationWithMultipleConsturcutors : ITestInterface
    {
        public TestInterfaceImplementationWithMultipleConsturcutors()
        {
        }

        public TestInterfaceImplementationWithMultipleConsturcutors(Dependency dependency)
        {
        }
    }
}