using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper.Exceptions
{

    /// <summary>
    /// 
    /// </summary>
    [global::System.Serializable]
    public class  NoOperationNameDefinedException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NoOperationNameDefinedException"/> class.
        /// </summary>
        public  NoOperationNameDefinedException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="NoOperationNameDefinedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public  NoOperationNameDefinedException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="NoOperationNameDefinedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public  NoOperationNameDefinedException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="NoOperationNameDefinedException"/> class.
        /// </summary>
        /// <param name="info">Clase <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> que contiene los datos serializados del objeto que hacen referencia a la excepción que se va a producir.</param>
        /// <param name="context">Enumeración <see cref="T:System.Runtime.Serialization.StreamingContext"></see> que contiene información contextual sobre el origen o el destino.</param>
        /// <exception cref="T:System.ArgumentNullException">El valor del parámetro info es null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">El nombre de la clase es null o <see cref="P:System.Exception.HResult"></see> es cero (0). </exception>
        protected  NoOperationNameDefinedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
