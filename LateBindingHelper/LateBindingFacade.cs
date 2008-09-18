using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LateBindingHelper
{
    /// <summary>
    /// Facade Design Pattern for simplifing.NET Late Binding calls    
    /// </summary>
    /// <example>
    /// This example shows how to call a method that returns an int with two parameters, one
    /// of them passed by reference:
    /// </example>
    public class LateBindingFacade : LateBindingHelper.ILateBindingFacade
    {
        /// <summary>
        /// Establish the type of binding that 
        /// .NET must perform on an late binding call
        /// </summary>
        public enum EOperationType
        {
            /// <summary>
            /// The operation calls a method
            /// </summary>
            METHOD,

            /// <summary>
            /// The operation sets the value for a property
            /// </summary>
            PROPERTY_GET,

            /// <summary>
            /// The operation retrieves a value for a property
            /// </summary>
            PROPERTY_SET,

            /// <summary>
            /// The operation gets the value for a field
            /// </summary>
            FIELD_GET,

            /// <summary>
            /// The operation sets the value for a field
            /// </summary>
            FIELD_SET
        }

        #region Static Properties

        /// <summary>
        /// Returns a string with the possible
        /// operations this class is capable of call
        /// </summary>
        static public string[] AvailableOperations
        {
            get { return Enum.GetNames(typeof(EOperationType)); }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Creates an Late binding facade instance using a previously created instance.
        /// </summary>
        /// <param name="obj">Instance of the type to be used</param>
        /// <exception cref="ArgumentException">
        /// If object is null
        /// </exception>
        public LateBindingFacade(object obj)
        {
            //Check error
            if (obj == null)
            {
                throw new ArgumentException(ErrorStrings.INVALID_OBJECT);
            }

            //Assign object
            _instance = obj;
        }

        /// <summary>
        /// Creates an late binding facade for a type.
        /// </summary>
        /// <param name="type">The type we'll be using to perform the late binding calls</param>
        /// <remarks>
        /// The Type must support a default constructor.
        /// </remarks>
        public LateBindingFacade(Type type)
        {
            if (type.GetConstructor(System.Type.EmptyTypes) == null)
                throw new ArgumentException(
                    string.Format("Type {0} does not have a default constructor", type.ToString()));

            _instance = Activator.CreateInstance(type);

        }

        /// <summary>
        /// Creates an late binding facade for a type.
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="parameters">The parameters for the constructor.</param>
        /// <remarks>
        /// The Type must support a default constructor.
        /// </remarks>
        public LateBindingFacade(Type type, params string[] parameters)
        {
            if (type.GetConstructor(System.Type.EmptyTypes) == null)
                throw new ArgumentException(
                    string.Format("Type {0} does not have a default constructor", type.ToString()));

            _instance = Activator.CreateInstance(type, parameters);

        }

        /// <summary>
        /// Calls an late binding object method using Late Binding.
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
        public void Call(string methodName, object[] args)
        {
            object retVal;

            CallOperation(
                methodName,
                args,
                out retVal,
                EOperationType.METHOD);
        }

        /// <summary>
        /// Calls an late binding object method using Late Binding, passing
        /// some or all the arguments by reference.
        /// </summary>
        /// <param name="methodName">Name of the method to be invoked.</param>
        /// <param name="byRef">Contains a list of booleans indicating if the which of the the args
        /// passed will be passed as reference.</param>
        /// <param name="args">List of parameters with the order and type
        /// required by the method invoked.</param>
        public void Call(string methodName, bool[] byRef, object[] args)
        {
            object retVal;

            if (byRef.Length > args.Length)
                throw new ArgumentOutOfRangeException("byRef array length is greater than the arguments array length!"); ;

            ParameterModifier refParams = new ParameterModifier(args.Length);
            for (int i = 0; i < byRef.Length && i < args.Length; ++i)
            {
                refParams[i] = byRef[i];
            }

            CallOperation(
                methodName,
                args,
                out retVal,
                refParams,
                EOperationType.METHOD);
        }

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
        public T Call<T>(string methodName, object[] args)
        {
            object retVal;

            CallOperation(
                methodName,
                args,
                out retVal,
                EOperationType.METHOD);

            if (retVal != null)
                return (T)retVal;

            else return default(T);
        }

        /// <summary>
        /// Calls an late binding object method using Late Binding, passing
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
        public T Call<T>(string methodName, bool[] byRef, object[] args)
        {
            if (byRef.Length > args.Length)
                throw new ArgumentOutOfRangeException("byRef array length is greater than the arguments array length!");

            object retVal;

            ParameterModifier refParams = new ParameterModifier(args.Length);
            for (int i = 0; i < byRef.Length && i < args.Length; ++i)
            {
                refParams[i] = byRef[i];
            }

            CallOperation(
                methodName,
                args,
                out retVal,
                refParams,
                EOperationType.METHOD);

            if (retVal != null)
                return (T)retVal;

            else return default(T);
        }
        
        /// <summary>
        /// Gets a value of a property using Late Binding in a strong-typed bias.
        /// </summary>
        /// <typeparam name="T">Type of the data to be returned by the property.</typeparam>
        /// <param name="propertyName">Name of the property to be accessed.</param>
        /// <returns>The value of the property, strong typed</returns>
        public T Get<T>(string propertyName)
        {
            object retVal;

            try
            {
                CallOperation(
                    propertyName,
                    null,
                    out retVal,
                    EOperationType.PROPERTY_GET);
            }

                //If not a property, try if is a field
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is MissingMethodException)
                {
                    CallOperation(
                        propertyName,
                        null,
                        out retVal,
                        EOperationType.FIELD_GET);
                }
                else throw;
            }

            return (T)retVal;
        }

        /// <summary>
        /// Gets the value of a property using Late Binding
        /// </summary>
        /// <param name="propertyName">Name of the property to be accessed.</param>
        /// <returns>The value of the property, as an object.</returns>
        public object Get(string propertyName)
        {
            return Get<object>(propertyName);
        }

        /// <summary>
        /// Gets the value of an indexer property given a specified index.
        /// </summary>
        /// <typeparam name="T">Type of the value to retrieve.</typeparam>
        /// <param name="index">The object used to index.</param>
        /// <returns>The object at the specified index.</returns>
        public T GetIndex<T>(object index)
        {
            object retVal;

            CallOperation("Item", new object[] { index }, out retVal, EOperationType.PROPERTY_GET);

            return (T)retVal;
        }

        /// <summary>
        /// Gets the value of an indexer property given a specified index.
        /// </summary>
        /// <param name="index">The object used to index.</param>
        /// <returns>The object at the specified index.</returns>
        public object GetIndex(object index)
        {
            return GetIndex<object>(index);
        }

        /// <summary>
        /// Sets the value at a specified index.
        /// </summary>
        /// <typeparam name="T">Type of the value to retrieve.</typeparam>
        /// <param name="index">The object used to index.</param>
        /// <param name="value">The new value at the specified index.</param>
        public void SetIndex<T>(object index, object value)
        {
            object retVal;

            CallOperation("Item", new object[] { index, value }, out retVal, EOperationType.PROPERTY_SET);
        }

        /// <summary>
        /// Sets the value at a specified index.
        /// </summary>
        /// <param name="index">The object used to index.</param>
        /// <param name="value">The new value at the specified index.</param>
        public void SetIndex(object index, object value)
        {
            SetIndex<object>(index, value);
        }

        /// <summary>
        /// Set an object property using Late Binding
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public bool Set(string propertyName, object value)
        {
            //No ret value, but we must pass an argument to the param
            object nullobj;

            bool result = false;

            try
            {
                result = CallOperation(
                    propertyName,
                    new object[] { value },
                    out nullobj,
                    EOperationType.PROPERTY_SET);
            }
            //If not a property, try if is a field
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is MissingMethodException)
                {
                    result = CallOperation(
                        propertyName,
                        new object[] { value },
                        out nullobj,
                        EOperationType.FIELD_SET);
                }
                else throw;
            }


            return result;

        }

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
        public bool CallOperation(
            string operationName,
            object[] args,
            out object retVal,
            ParameterModifier refParams,
            EOperationType type)
        {
            //Error check
            if (_instance == null)
                throw new NullReferenceException("Object has not been properly initialized.");

            //Must make an assignation 'cause retVal is marked with 'out'
            retVal = null;

            //Bad method name
            if (operationName == string.Empty)
            {
                throw new ArgumentException(ErrorStrings.LATEBINDING_EMPTY_CALL_STRING);
            }

            StackFrame sf = new StackFrame(true);

            //Do late call
            try
            {

                retVal = _instance.GetType().InvokeMember(operationName,
                                        ComputeBindingFlags(type),
                                        null,
                                        _instance,
                                        args,
                                        new ParameterModifier[] { refParams },
                                        null,
                                        null);
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException is COMException)
                {
                    throw new Exception(
                        ErrorStrings.LATEBINDING_CALL_FAILED + " " + e.Message,
                        Marshal.GetExceptionForHR((e.InnerException as COMException).ErrorCode));
                }

                throw new Exception(ErrorStrings.LATEBINDING_CALL_FAILED + " " + e.Message, e);
            }

            //All OK
            return true;

        }

        /// <summary>
        /// Calls an operation of specifyed name, with arguments.
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
        public bool CallOperation(
           string operationName,
           object[] args,
           out object retVal,
           EOperationType operationType)
        {
            if (_instance == null)
                throw new Exception(ErrorStrings.LATEBINDING_OBJECT_NOT_SET);

            //Must assign this
            retVal = null;

            //Bad method name
            if (operationName == string.Empty)
                throw new ArgumentException(ErrorStrings.LATEBINDING_EMPTY_CALL_STRING);

            StackFrame sf = new StackFrame(true);
            BindingFlags bFlags = ComputeBindingFlags(operationType);

            //Do call
            try
            {

                //Do late binding call
                retVal = _instance.GetType().InvokeMember(operationName,
                                                            bFlags,
                                                            null,
                                                            _instance,
                                                            args);
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException is COMException)
                {
                    throw new Exception(
                        ErrorStrings.LATEBINDING_CALL_FAILED + " " + e.Message,
                        Marshal.GetExceptionForHR((e.InnerException as COMException).ErrorCode));
                }

                throw new Exception(ErrorStrings.LATEBINDING_CALL_FAILED + " " + e.Message, e);
            }

            //All OK
            return true;
        }


        #endregion

        #region Non-public Methods

        /// <summary>
        /// Generates the correct bindingFlag necessary for a late binding call
        /// given an <see cref="EOperationType"/> type.
        /// </summary>
        /// <param name="type">
        /// <see cref="EOperationType"/> type
        /// </param>
        /// <returns>
        /// Correct binding flags for that type call, added to
        /// BindingFlags.Public | BindingFlags.Static flags
        /// </returns>
        private BindingFlags ComputeBindingFlags(EOperationType type)
        {

            switch (type)
            {
                case EOperationType.METHOD:
                    return BindingFlags.InvokeMethod;

                case EOperationType.PROPERTY_GET:
                    return BindingFlags.GetProperty;

                case EOperationType.PROPERTY_SET:
                    return BindingFlags.SetProperty;

                case EOperationType.FIELD_SET:
                    return BindingFlags.SetField;
                
                case EOperationType.FIELD_GET:
                    return BindingFlags.GetField;
            }

            return BindingFlags.Default;

        }

        #endregion

        #region Non-Public Members
        /// <summary>
        /// Automatión object necessary for late binding
        /// </summary>
        protected object _instance;


        #endregion

        #region Object Methods Overrides

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
        public override bool Equals(object obj)
        {
            return _instance.GetHashCode() == obj.GetHashCode();
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return this._instance.GetHashCode();
        }
        #endregion
    }

}
