using System;
using System.Collections.Generic;
using System.Text;

using LateBindingHelper.Implementation;
using System.Reflection;

namespace LateBindingHelper
{
    /// <summary>
    /// <para> Factory DP </para>
    /// Creates bindings of IInvoker instances to objects
    /// </summary>
    public static class BindingFactory
    {
        /// <summary>
        /// Creates a <see cref="IOperationInvoker"/> instance binded to a object instance.
        /// </summary>
        /// <returns>
        /// A new <see cref="IOperationInvoker"/> instance.
        /// </returns>
        public static IOperationInvoker CreateObjectBinding(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Late Binding obj is null");
            return new Invoker(obj);
        }

        /// <summary>
        /// Creates a <see cref="IOperationInvoker"/> instance binded to a new instance of a type.
        /// </summary>
        /// <returns>
        /// A new <see cref="IOperationInvoker"/> instance.
        /// </returns>
        public static IOperationInvoker CreateObjectBinding(Type lbType)
        {
            if (lbType == null)
                throw new ArgumentNullException("Late Binding type is null");
            return new Invoker(Activator.CreateInstance(lbType));
        }

        /// <summary>
        /// Creates a new <see cref="IOperationInvoker"/> instance binded to a internal created 
        /// instance of a type using the specified arguments to the constructor
        /// </summary>
        /// <param name="lbType">Type of the object to instantiate</param>
        /// <param name="args">Arguments for the type constructor</param>
        /// <returns>
        /// A new <see cref="IOperationInvoker"/> instance.
        /// </returns>
        public static IOperationInvoker CreateObjectBinding(Type lbType, params object[] args)
        {
            if (lbType == null)
                throw new ArgumentNullException("Late Binding type is null");
            return new Invoker(Activator.CreateInstance(lbType, args));
        }


        /// <summary>
        /// Creates a new <see cref="IOperationInvoker"/> instance binded to a internal created 
        /// instance of a type using the specified arguments to the constructor
        /// </summary>
        /// <param name="assemblyName">Name of the assembly which contains the type to be instantiated.</param>
        /// <param name="typeName">Full name of the type to be instantiated.</param>
        /// <returns>
        /// A new <see cref="IOperationInvoker"/> instance.
        /// </returns>
        public static IOperationInvoker CreateObjectBinding(string assemblyName, string typeName)
        {
            if (assemblyName == null || assemblyName == string.Empty)
                throw new ArgumentNullException("Invalid assembly name");

            if (typeName == null || typeName == string.Empty)
                throw new ArgumentNullException("Invalid type name");

            Type lbType = Assembly.Load(assemblyName).GetType(typeName, false);

            return new Invoker(Activator.CreateInstance(lbType));
        }

        /// <summary>
        /// Creates a new <see cref="IOperationInvoker"/> instance binded to a internal created 
        /// instance of a type using the specified arguments to the constructor
        /// </summary>
        /// 
        /// <param name="assemblyName">Type of the object to instantiate</param>
        /// <param name="typeName">Full name of the type to be instantiated.</param>
        /// <param name="args">Arguments for the type constructor</param>
        public static IOperationInvoker CreateObjectBinding(string assemblyName, string typeName, params object[] args)
        {
            if (assemblyName == null || assemblyName == string.Empty)
                throw new ArgumentNullException("Invalid assembly name");

            if (typeName == null || typeName == string.Empty)
                throw new ArgumentNullException("Invalid type name");

            //TODO: Obsolete but needed to avoid passing the complete assembly name (which includes
            //assembly version and public key
            Type lbType = Assembly.LoadWithPartialName(assemblyName).GetType(typeName, false);

            return new Invoker(Activator.CreateInstance(lbType, args));
        }

        /// <summary>
        /// Creates a <see cref="IOperationInvoker"/> instance binded to a new instance of 
        /// the automation object referenced by the namep <paramref name="objectName"/>
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns>
        /// A new <see cref="IOperationInvoker"/> instance.
        /// </returns>
        public static IOperationInvoker CreateAutomationBinding(string objectName)
        {
            if (objectName == null || objectName == string.Empty)
                throw new ArgumentNullException("Invalid object name.");

            Type objectType = Type.GetTypeFromProgID(objectName);

            return new Invoker(Activator.CreateInstance(objectType));
        }
    }
}
