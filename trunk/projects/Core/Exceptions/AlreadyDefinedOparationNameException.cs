using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper.Exceptions
{
    /// <summary>
    /// Thrown if an operation name was already defined when performing a late binding call over an instance.
    /// </summary>
    [global::System.Serializable]
    public class AlreadyDefinedOparationNameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyDefinedOparationNameException"/> class.
        /// </summary>
        public AlreadyDefinedOparationNameException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyDefinedOparationNameException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public AlreadyDefinedOparationNameException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyDefinedOparationNameException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public AlreadyDefinedOparationNameException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyDefinedOparationNameException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected AlreadyDefinedOparationNameException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
