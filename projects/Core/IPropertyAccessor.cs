using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper
{
    /// <summary>
    /// Provides an interface for specifying a property to access on a type 
    /// </summary>
    public interface IPropertyAccessor
    {
        /// <summary>
        /// Selects the property over which we will invoke an operation.
        /// </summary>
        /// <param name="propertyName">String with the name of the property to be invoked</param>
        /// <returns> 
        /// An <see cref="IGetSetInvoker"/> that will establish the operation to
        /// perform over the property specifyed by the propertyName parameter.
        /// </returns>
        IGetSetInvoker Property(string propertyName);
    }
}
