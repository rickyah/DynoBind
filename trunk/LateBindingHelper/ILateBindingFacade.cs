using System;
namespace LateBindingHelper
{
    public interface ILateBindingFacade
    {
        /// <summary>
        /// Calls an object method, which returns a value, using Late Binding.
        /// </summary>
        /// <remarks>
        /// If the invoked method does not originally returned a value,
        /// then a default(T) value is returned by this method.
        /// </remarks>
        /// <typeparam name="T">Type of the data returned by the method.</typeparam>
        /// <param name="methodName">Name of the method to be invoked.</param>
        /// <returns>The return value of the operation casted to the type parameter T</returns>
        T Call<T>(string methodName);

        /// <summary>
        /// Calls an object method, which returns a value, using Late Binding.
        /// </summary>
        /// <remarks>
        /// If the invoked method does not originally returned a value,
        /// then a default(T) value is returned by this method.
        /// </remarks>
        /// <typeparam name="T">Type of the data returned by the method.</typeparam>
        /// <param name="methodName">Name of the method to be invoked.</param>
        /// <param name="args">
        /// List of parameters passed to the invocation. They must have same order and type
        /// expected by the method invoked.
        /// </param>
        /// <returns>The return value of the operation casted to the type parameter T</returns>
        T Call<T>(string methodName, object[] args);

        /// <summary>
        /// Calls an operation over an object using Late Binding passing
        /// some or all the arguments by reference and also returning a value.
        /// </summary>
        /// <remarks>
        /// If the invoked method does not originally returned a value,
        /// then a default(T) value is returned by this method.
        /// </remarks>
        /// <typeparam name="T">Type of the data returned by the method.</typeparam>
        /// <param name="methodName">Name of the method to be invoked.</param>
        /// <param name="byRef">Contains a list of booleans indicating if the which of the the args
        /// passed will be passed as reference.</param>
        /// <param name="args">
        /// List of parameters passed to the invocation. They must have same order and type
        /// expected by the method invoked.
        /// </param>
        /// <returns>The return value of the operation casted to the type parameter T</returns>
        T Call<T>(string methodName, bool[] byRef, object[] args);
        
        /// <summary>
        /// Gets the value of an indexer property given a specified index.
        /// </summary>
        /// <typeparam name="T">Type of the value to retrieve.</typeparam>
        /// <param name="index">The object used to index.</param>
        /// <returns>The object at the specified index.</returns>
        T GetIndex<T>(object index);

        /// <summary>
        /// Gets the value of an indexer property given a specified index.
        /// </summary>
        /// <param name="index">The object used to index.</param>
        /// <returns>The object at the specified index.</returns>
        object GetIndex(object index);

        /// <summary>
        /// Calls an operation over an object using Late Binding.
        /// </summary>
        /// <remarks>
        /// This method call does not return a value, even if the method invoked
        /// using late binding does.
        /// </remarks>
        /// <param name="methodName">Name of the method to be invoked</param>
        void Call(string methodName);

        /// <summary>
        /// Calls an operation over an object using Late Binding.
        /// </summary>
        /// <remarks>
        /// This method call does not return a value, even if the method invoked
        /// using late binding does.
        /// </remarks>
        /// <param name="methodName">Name of the method to be invoked</param>
        /// <param name="args">
        /// List of parameters with the order and type
        /// required by the method invoked.
        /// </param>
        void Call(string methodName, object[] args);

        /// <summary>
        /// Calls an operation over an object using Late Binding
        /// passing some or all the arguments by reference.
        /// </summary>
        /// <param name="methodName">Name of the method to be invoked.</param>
        /// <param name="byRef">Contains a list of booleans indicating if the which of the the args
        /// passed will be passed as reference.</param>
        /// <param name="args">List of parameters with the order and type
        /// required by the method invoked.</param>
        void Call(string methodName, bool[] byRef, object[] args);

        /// <summary>
        /// Calls an operation of specified name, with some arguments.
        /// Operation in this context is any method or property in
        /// an object
        /// </summary>
        /// <param name="operationName">Name of operation to be called</param>
        /// <param name="args">Args for this operation call. Must be passed as an array of
        /// objects, or null if the method doesn't need arguments</param>
        /// <param name="retVal">Object containing the operation return value, or null if
        /// method returns nothing.
        /// The object must be casted by the user to the correct type</param>
        /// <param name="operationType">Type of the operation.</param>
        /// <returns>
        /// Bool if call was succesfull, false if some error ocurred.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Throwed if any argument is incorrect
        /// </exception>
        /// <exception cref="Exception">
        /// Throwed if the call fails
        /// </exception>
        bool CallOperation(string operationName, object[] args, out object retVal, EOperationType operationType);
        
        /// <summary>
        /// Calls an operation of specifyed name, with arguments.
        /// Operation in this context is any method or property in
        /// an object
        /// </summary>
        /// <param name="operationName">
        /// Name of operation to be called</param>
        /// <param name="args">
        /// Args for this operation call. Must be passed as an array of 
        /// objects, or null if the method doesn't need arguments
        /// </param>
        /// <param name="retVal">
        /// Object containing the operation return value, or null if 
        /// method returns nothing.
        /// The object must be casted by the user to the correct type
        /// </param>
        /// <param name="refParams">
        /// Indicates if any of the arguments must be passed by reference 
        /// </param>
        /// <param name="type">
        /// Value from <see cref="EOperationType"/> representing
        /// the type of call we are making
        /// </param>
        /// <returns>
        /// Bool if call was succesfull, false if some error ocurred. 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Throwed if any argument is incorrect
        /// </exception>
        /// <exception cref="Exception">
        /// Throwed if the call fails
        /// </exception>
        bool CallOperation(string operationName,
            object[] args,
            out object retVal,
            System.Reflection.ParameterModifier refParams,
            EOperationType type);

        /// <summary>
        /// Gets a value of a property using Late Binding in a strong-typed bias.
        /// </summary>
        /// <typeparam name="T">Type of the data to be returned by the property.</typeparam>
        /// <param name="propertyName">Name of the property to be accessed.</param>
        /// <returns>The value of the property, strong typed</returns>
        T Get<T>(string propertyName);

        /// <summary>
        /// Gets the value of a property using Late Binding
        /// </summary>
        /// <param name="propertyName">Name of the property to be accessed.</param>
        /// <returns>The value of the property, as an object.</returns>
        object Get(string propertyName);

        /// <summary>
        /// Sets the value at a specified index.
        /// </summary>
        /// <typeparam name="T">Type of the value to retrieve.</typeparam>
        /// <param name="index">The object used to index.</param>
        /// <param name="value">The new value at the specified index.</param>
        void SetIndex<T>(object index, object value);

        /// <summary>
        /// Sets the value at a specified index.
        /// </summary>
        /// <param name="index">The object used to index.</param>
        /// /// <param name="value">The new value at the specified index.</param>
        void SetIndex(object index, object value);

        /// <summary>
        /// Set an object property using Late Binding
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        bool Set(string propertyName, object value);
    }
}
