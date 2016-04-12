using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeTi
{
    /// <summary>
    /// IOC container
    /// </summary>
    public class YeTiContainer
    {
        public void Register<TRegistration, TImplementation>() where TImplementation : TRegistration, new()
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>()
        {
            throw new NotImplementedException();
        }
    }
}