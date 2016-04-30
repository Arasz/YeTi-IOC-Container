using Shouldly;
using Xunit;
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
    public class YeTiContainerTest
    {
        private readonly YeTiContainer _container;

        public YeTiContainerTest()
        {
            _container = new YeTiContainer();
        }

        /// <summary>
        /// Contains method tested by this test class 
        /// </summary>
        /// <returns></returns>
        public ITestInterface Act() => _container.Resolve<ITestInterface>();

        /// <exception cref="ThrowsException">
        /// Thrown when an exception was not thrown, or when an exception of the incorrect type is thrown
        /// </exception>
        [Fact]
        public void ResolvesComponentWithMoreThenOneConstructor_RegisteredTypeAsGenericParameters_Throws()
        {
            _container.Register<ITestInterface, TestInterfaceImplementationWithMultipleConstructors>();
            _container.Register<Dependency, Dependency>();

            var exception = Record.Exception(() => Act());

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ComponentHasMultipleConstructorsException>();
        }

        [Fact]
        public void ResolvesRegisteredComponent_RegisteredTypeAsGenericParameter_CreatedObjectOfGivenType()
        {
            _container.Register<ITestInterface, TestInterfaceImplementation>();

            var resolvedObject = Act();

            resolvedObject.ShouldBeOfType<TestInterfaceImplementation>();
        }

        [Fact]
        public void
            ResolvesRegisteredComponentsWithParameters_RegisteredTypeAsGenericParameterAndParameters_CreatedObjectOfGivenType
            ()
        {
            _container.Register<ITestInterface, TestImplementationWithDependency>();
            _container.Register<Dependency, Dependency>();

            var resolvedObject = Act();

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

    internal class TestInterfaceImplementationWithMultipleConstructors : ITestInterface
    {
        public TestInterfaceImplementationWithMultipleConstructors()
        {
        }

        public TestInterfaceImplementationWithMultipleConstructors(Dependency dependency)
        {
        }
    }
}