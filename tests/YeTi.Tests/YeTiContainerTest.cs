using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeTi.Tests
{
    /// <summary>
    /// YeTi IOC container test
    /// <remarks>
    /// Tests naming convention [UnitOfWork_StateUnderTest_ExpectedBehavior]
    /// </remarks>
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
    }

    public interface ITestInterface
    {
    }

    internal class TestInterfaceImplementation : ITestInterface
    {
    }
}