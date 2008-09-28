using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LateBindingHelper
{
    public static class LateBindingFactory
    {

        /// <summary>
        /// Creates a <see cref="ILateBindingFacade"/> instance binded to a object instance.
        /// </summary>
        /// <remarks></remarks>
        public static ILateBindingFacade CreateBinding(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Late Binding obj is null");
            return new LateBindingFacade(obj);
        }

        /// <summary>
        /// Creates a <see cref="ILateBindingFacade"/> instance binded to a new instance of a type.
        /// </summary>
        /// <remarks></remarks>
        public static ILateBindingFacade CreateBinding(Type lbType)
        {
            if (lbType == null)
                throw new ArgumentNullException("Late Binding type is null");
            return new LateBindingFacade( Activator.CreateInstance(lbType));
        }

       /// <summary>
        /// Creates a <see cref="ILateBindingFacade"/> instance binded to a new instance of a type,
        /// and using argumetns
        /// </summary>
        /// <param name="lbType">Type of the object to instanciate</param>
        /// <param name="args">Arguments for the type constructor</param>
        public static ILateBindingFacade CreateBinding(Type lbType, params object[] args)
        {
            if (lbType == null)
                throw new ArgumentNullException("Late Binding type is null");
            return new LateBindingFacade(Activator.CreateInstance(lbType, args ));
        }

       /// <summary>
        /// Creates a <see cref="ILateBindingFacade"/> instance binded to a new instance of a type.
        /// </summary>
        public static ILateBindingFacade CrateAutomationBinding(string objectName)
        {
            if (objectName == null || objectName == string.Empty)
                throw new ArgumentNullException("Invalid object name.");

            Type objectType = Type.GetTypeFromProgID(objectName);
            return new LateBindingFacade( Activator.CreateInstance(objectType) );
        }
    }
}
