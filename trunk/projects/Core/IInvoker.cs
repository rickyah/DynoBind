using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper
{
    /// <summary>
    /// Implements an interface to perform operations over a type using
    /// late binding calls
    /// </summary>
    public interface IOperationInvoker : 
        IPropertyAccessor,
        IMethodInvoker,
        IMethodAccessor,
        IIndexerAccessor,
        IFieldAccessor
    {
        /// <summary>
        /// Object which recieves the late binding calls
        /// </summary>
        object InstanceObject
        {
            get;
        }
    }
}
