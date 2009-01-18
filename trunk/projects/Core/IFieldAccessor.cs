using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper
{
    /// <summary>
    /// Provides an interface for specifying a field to access on a type 
    /// </summary>
    public interface IFieldAccessor : IGetSetInvoker
    {
        /// <summary>
        /// Selects the field over which we will invoke an operation.
        /// </summary>
        /// <param name="fieldName">String with the name of the field to be invoked</param>
        /// <returns> 
        /// An <see cref="IGetSetInvoker"/> that will establish the operation to
        /// perform over the field specifyed by the fieldName parameter.
        /// </returns>
        IGetSetInvoker Field (string fieldName);
    }
}
