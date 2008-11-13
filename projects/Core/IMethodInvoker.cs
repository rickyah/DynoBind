using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper
{

    /// <summary>
    /// Provides an interface which will allow adding parameters to a method defined
    /// bya a <see cref="IMethdoAccessor"/>,  and invoke that method
    /// </summary>
    public interface IMethodInvoker
    {
        /// <summary>
        /// Adds a parameter to the method call, passed with value semantics
        /// </summary>
        /// <param name="value">object to be passed as parameter</param>
        /// <returns>
        /// A reference to the object which made this operation
        /// </returns>
        IMethodInvoker AddParameter(object value);

        /// <summary>
        /// Adds a parameter to the method call, passed with reference semantics
        /// <param name="value">object to be passed as parameter</param>
        /// <returns>
        /// A reference to the object which made this operation
        /// </returns>
        IMethodInvoker AddParameter(ref object value);

        /// <summary>
        /// Performs the call to the method defined by a previous <see cref="IMethodAccessor.Method"/>
        /// call, with the parameters specified by the <see cref="IMethodInvoker.AddParameter"/> calls
        /// casting the return value to the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the data returned by the method call</typeparam>
        /// <returns>
        /// The data returned by the method call, casted to the <typeparamref name="T">T type param</typeparamref> 
        /// </returns>
        /// <remarks>
        /// The type parameter T must match the type of data returned by the method call 
        /// access or an exception will be throw.
        /// </remarks>
        /// <exception cref="InvalidCastException">
        /// If the type parameter T does not match the type of data returned, thus a casting 
        /// could not be performed
        /// </exception>
        T Invoke<T>();

        /// <summary>
        /// Performs the call to the method which was defined by a previous <see cref="IMethodAccessor.Method"/>
        /// call, with the parameters specified by the <see cref="IMethodInvoker.AddParameter"/> calls
        /// The Method called either has no return parameters or they will be not needed.
        /// </summary>
        void Invoke();

        /// <summary>
        /// Returns the value of the parameters after the call to any
        /// <see cref="IMethodInvoker.Invoke"/> method
        /// Only usefull to retrieve the values of the call that were passed as
        /// reference, and thus has been potentially modified.
        /// </summary>
        /// <remarks>
        /// All parameters passed to the method are referenced after the call to an
        /// <see cref="IMethodInvoker.Invoke"/> method, either they were passed as
        /// reference or not.
        /// </remarks>
        object[] LastCallParameters { get;}
    }
}
