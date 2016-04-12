using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YeTi
{
    /// <summary>
    /// IOC container
    /// </summary>
    public class YeTiContainer
    {
        private readonly Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();

        /// <summary>
        /// Registers type to create
        /// </summary>
        /// <typeparam name="TRegistration">Registered type</typeparam>
        /// <typeparam name="TImplementation">Type of object which will be created</typeparam>
        public void Register<TRegistration, TImplementation>() where TImplementation : TRegistration, new()
        {
            _registrations[typeof(TRegistration)] = typeof(TImplementation);
        }

        /// <summary>
        /// Resolves given type to registered object
        /// </summary>
        /// <typeparam name="T">Type of object which we want to get</typeparam>
        /// <returns>Object of type <typeparamref name="T"/></returns>
        public T Resolve<T>() where T : class
        {
            var requestedType = typeof(T);
            var actualType = _registrations[requestedType];

            var instance = Activator.CreateInstance(actualType) as T;

            return instance;
        }
    }
}