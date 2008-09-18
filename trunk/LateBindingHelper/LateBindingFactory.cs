using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper
{
    public static class LateBindingFactory
    {
        /// <summary>
        /// Creates a <see cref="ILateBindingFacade"/> instance binded to a new instance of a type.
        /// </summary>
        public static ILateBindingFacade CreateBinding(Type lbType)
        {
            if (lbType == null)
                throw new ArgumentNullException("Late Binding type is null");
            return new LateBindingFacade( Activator.CreateInstance(lbType));
        }

        public static ILateBindingFacade CreateBinding(Type lbType, params object[] args)
        {
            if (lbType == null)
                throw new ArgumentNullException("Late Binding type is null");
            return new LateBindingFacade(Activator.CreateInstance(lbType, args ));
        }
    }
}
