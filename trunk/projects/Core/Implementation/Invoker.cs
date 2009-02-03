using System;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using LateBindingHelper;
using LateBindingHelper.Exceptions;
using LateBindingHelper.Implementation;


namespace LateBindingHelper.Implementation
{
    /// <summary>
    /// Implementation for IInvoker
    /// </summary>
    internal class Invoker : 
        IObjectOperation, 
        IMethodOperations, 
        IGetSetOperations
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of the type Invoker
        /// </summary>
        /// <param name="invocationObject">object where the late binding calls will be made</param>
        internal Invoker(object invocationObject)
        {
            InstanceObject = invocationObject;
        }

        #endregion

        #region IInvoker Members

        /// <summary>
        /// Object which recieves the late binding calls
        /// </summary>
        public object InstanceObject
        {
            get { return _instanceObject; }
            private set { _instanceObject = value; }
        }

        #endregion

        #region IFieldAccessor Members

        /// <summary>
        /// Selects the field over which we will invoke an operation.
        /// </summary>
        /// <param name="fieldName">String with the name of the field to be invoked</param>
        /// <returns> 
        /// An <see cref="IGetSetInvoker"/> that will establish the operation to
        /// perform over the field specifyed by the fieldName parameter.
        /// </returns>
        public IGetSetOperations Field(string fieldName)
        {
            if (OperationName != string.Empty)
                throw new AlreadyDefinedOperationNameException();

            OperationName = fieldName;
            OperationType = EGetSetInvokerOperation.Field;
            
            return this;
        }

        #endregion

        #region IMethodAccessor Members

        /// <summary>
        /// Sets the Method name to be invoked
        /// </summary>
        /// <param name="methodName">String with the name of the method to be invoked</param>
        /// <returns> 
        /// An <see cref="IMethodInvoker"/> that will establish the parameters to
        /// add to this method invocation, and also will allow to perform the method real invocation.
        /// </returns>
        public IMethodOperations Method(string methodName)
        {
            if (OperationName != string.Empty)
                throw new AlreadyDefinedOperationNameException();

            OperationName = methodName;

            return this;
        }

        #endregion

        #region IMethodInvoker Members

        /// <summary>
        /// Adds a parameter to the method call, passed with value semantics
        /// </summary>
        /// <param name="value">object to be passed as parameter</param>
        /// <returns>
        /// A reference to the object which made this operation
        /// </returns>
        public IMethodOperations AddParameter(object value)
        {
            InnerParameterBuilder.AddParameter(value);

            return this;
        }

        /// <summary>
        /// Adds a parameter to the method call, passed with reference semantics
        /// </summary>
        /// <param name="value">object to be passed as parameter</param>
        /// <returns>
        /// A reference to the instance which called this operation.
        /// </returns>
        public IMethodOperations AddRefParameter(object value)
        {
            InnerParameterBuilder.AddRefParameter(value);

            return this;
        }

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
        public T Invoke<T>()
        {
            object retValue;
            object[] args;

            args = InnerParameterBuilder.GetParametersAsArray();

            if (InnerParameterBuilder.Count <= 0)
            {
                CommonLateBindingOperations.CallOperation(
                    InstanceObject,
                    OperationName,
                    args,
                    out retValue,
                    EOperationType.Method);
            }
            else
            {
                ParameterModifier refParams = new ParameterModifier(args.Length);
                for (int i = 0; i < args.Length; ++i)
                {
                    refParams[i] = InnerParameterBuilder.GetReferenceParameterList()[i];
                }

                CommonLateBindingOperations.CallOperation(
                    InstanceObject,
                    OperationName,
                    args,
                    out retValue,
                    refParams,
                    EOperationType.Method);
            }

            LastCallParameters = args;

            ClearCall();

            return CommonLateBindingOperations.ComputeReturnType<T>(retValue);
        }

        /// <summary>
        /// Performs the call to the method which was defined by a previous <see cref="IMethodAccessor.Method"/>
        /// call, with the parameters specified by the <see cref="IMethodInvoker.AddParameter"/> calls
        /// The Method called either has no return parameters or they will be not needed.
        /// </summary>
        public IObjectOperation Invoke()
        {
            return Invoke<IObjectOperation>();
        }

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
        public object[] LastCallParameters
        {
            get { return _lastCallParameters; }
            private set { _lastCallParameters = value; }
        }



        #endregion

        #region IPropertyAccessor Members

        /// <summary>
        /// Selects the property over which we will invoke an operation.
        /// </summary>
        /// <param name="propertyName">String with the name of the property to be invoked</param>
        /// <returns> 
        /// An <see cref="IGetSetInvoker"/> that will establish the operation to
        /// perform over the property specifyed by the propertyName parameter.
        /// </returns>
        public IGetSetOperations Property(string propertyName)
        {
            if (OperationName != string.Empty)
                throw new AlreadyDefinedOperationNameException();

            OperationName = propertyName;
            OperationType = EGetSetInvokerOperation.Property;

            return this;
        }

        #endregion

        #region IGetSetAccessor Members

        /// <summary>
        /// Performs a Get operation to retrieve data, and returns it casted to the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the data accessed</typeparam>
        /// <returns>
        /// The data accessed, casted to the <typeparamref name="T">T type param</typeparamref> 
        /// </returns>
        /// <remarks>
        /// The type parameter T must match the type of data we are trying to 
        /// access or an exception will be throw.
        /// </remarks>
        /// <exception cref="InvalidCastException">
        /// If the type parameter T does not match the type of data accessed, thus a casting 
        /// could not be performed
        /// </exception>
        public T Get<T>()
        {
            if (OperationName == string.Empty)
                throw new NoOperationNameDefinedException();

            object retVal = null;
            object[] args = null;
            EOperationType op;

            if (OperationType == EGetSetInvokerOperation.Index)
            {
                op = EOperationType.PropertyGet;
                args = InnerParameterBuilder.GetParametersAsArray();
            }
            else
            {
                op = (OperationType == EGetSetInvokerOperation.Property) ?
                        EOperationType.PropertyGet
                       : EOperationType.FieldGet;
            }

            CommonLateBindingOperations.CallOperation(
                InstanceObject,
                OperationName,
                args,
                out retVal,
                op);

            ClearCall();

            return CommonLateBindingOperations.ComputeReturnType<T>(retVal);
        }

        /// <summary>
        /// Performs a Get operation to retrieve data.
        /// </summary>
        /// <returns>
        /// The data accessed as an <see cref="object"/>
        /// </returns>
        public IObjectOperation Get()
        {
            return Get<IObjectOperation>();
        }

        /// <summary>
        /// Performs a Set operation to modify data
        /// </summary>
        /// <param name="obj">New value </param>
        /// <remarks>
        /// The type of data passed as parameter 
        /// must match the type of data we are trying to modify or an 
        /// exception will be throw.
        /// </remarks>
        public void Set(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            if (OperationName == string.Empty)
                throw new NoOperationNameDefinedException();

            object retVal;
            object[] args;
            EOperationType op;


            if (OperationType == EGetSetInvokerOperation.Index)
            {
                op = EOperationType.PropertySet;
                List<object> tmp = new List<object>(InnerParameterBuilder.GetParametersAsArray());
                tmp.Add(obj);
                args = tmp.ToArray();
            }
            else
            {
                op = OperationType == EGetSetInvokerOperation.Property ?
                        EOperationType.PropertySet
                       : EOperationType.FieldSet;
                args = new object[] { obj };
            }

            CommonLateBindingOperations.CallOperation(
                InstanceObject,
                OperationName,
                args,
                out retVal,
                op);


            ClearCall();
        }

        #endregion

        #region IIndexerAccessor Members

        /// <summary>
        /// Performs indexer access over a type.
        /// </summary>
        /// <param name="indexList">object used as indexer</param>
        /// <returns>
        /// An <see cref="IGetSetInvoker"/> that will establish the operation to
        /// perform over the specifyed index.
        /// </returns>
        public IGetSetOperations Index(params object[] indexList)
        {
            if (OperationName != string.Empty)
                throw new AlreadyDefinedOperationNameException();

            OperationName = "Item";
            OperationType = EGetSetInvokerOperation.Index;
            foreach(object idx in indexList)
                InnerParameterBuilder.AddParameter(idx);

            return this;
        }

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
            return this.InstanceObject.GetHashCode() == obj.GetHashCode();
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return this.InstanceObject.GetHashCode();
        }
        #endregion

        #region Non-Public Methods

        /// <summary>
        /// Once a late binding call is performed after calling Invoke, Get or Set methods,
        /// reinitialices the structures to allow making a distint call
        /// </summary>
        private void ClearCall()
        {
            OperationType = null;
            OperationName = string.Empty;
            InnerParameterBuilder.Clear();
        }

        #endregion

        #region Non-Public Properties

        /// <summary>
        /// Where the parameters added by a AddParameter call
        /// will be stored before the late binding call is made 
        /// </summary>
        private IParameterBuilder InnerParameterBuilder
        {
            get { return _innerParameterBuilder; }
        }

        /// <summary>
        /// Name of the method/property/field that will be 
        /// made next
        /// </summary>
        private string OperationName
        {
            get { return _operationName; }
            set { _operationName = value; }
        }

        /// <summary>
        /// Element where the next get /set call will be performed 
        /// </summary>
        private EGetSetInvokerOperation? OperationType
        {
            get { return _operationType; }
            set { _operationType = value; }
        }

        #endregion

        #region Non-Public Members

        /// <summary>
        /// Defines the element where the next get /set 
        /// call will be performed
        /// </summary>
        private enum EGetSetInvokerOperation
        {
            Field,
            Property,
            Index
        }

        /// <summary>
        /// Defines the element where the next get /set 
        /// call will be performed
        /// </summary>
        private EGetSetInvokerOperation? _operationType;
        /// <summary>
        /// Name of the method/property/field that will be 
        /// made next
        /// </summary>
        private string _operationName = string.Empty;
        //TODO: Decoupling ?

        /// <summary>
        /// Where the parameters added by a AddParameter call
        /// will be stored before the late binding call is made 
        /// </summary>
        private IParameterBuilder _innerParameterBuilder = new ParameterBuilder();

        /// <summary>
        /// Value of the parameters after the call to any
        /// <see cref="IMethodInvoker.Invoke"/> method
        /// Only usefull to retrieve the values of the call that were passed as
        /// reference, and thus has been potentially modified.
        /// </summary>
        private object[] _lastCallParameters;

        /// <summary>
        /// Object which recieves the late binding calls
        /// </summary>
        private object _instanceObject;
        #endregion


    }
}