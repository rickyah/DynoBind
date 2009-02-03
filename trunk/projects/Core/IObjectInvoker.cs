using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper
{
    /// <summary>
    /// Implements an interface to perform operations over a type using
    /// late binding calls
    /// </summary>
    public interface IObjectInvoker : 
        IPropertyCall,
        IMethodCall,
        IIndexerCall,
        IFieldCall,
        IOperationLastCallParameters
    {

        /// <summary>
        /// Object which receives the late binding calls
        /// </summary>
        object InstanceObject
        {
            get;
        }
    }
}
