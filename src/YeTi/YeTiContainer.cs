using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <typeparam name="TRegistration"> Registered type </typeparam>
        /// <typeparam name="TImplementation"> Type of object which will be created </typeparam>
        public void Register<TRegistration, TImplementation>() where TImplementation : class, TRegistration
        {
            _registrations[typeof(TRegistration)] = typeof(TImplementation);
        }

        /// <summary>
        /// Resolves given type to registered object 
        /// </summary>
        /// <typeparam name="T"> Type of object which we want to get </typeparam>
        /// <returns> Object of type <typeparamref name="T"/> </returns>
        public T Resolve<T>() where T : class
        {
            return Resolve(typeof(T)) as T;
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object Resolve(Type type)
        {
            var requestedType = type;
            var actualType = _registrations[requestedType];

            var constructors = actualType.GetConstructors();

            var chosenConstructor = constructors.First();

            var dependencyTypes = chosenConstructor.GetParameters().Select(param => param.ParameterType);

            var dependencies = dependencyTypes.Select(Resolve).ToArray();

            var instance = Activator.CreateInstance(actualType, dependencies);

            return instance;
        }
    }
}