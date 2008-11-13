using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper.Exceptions
{
    
    [global::System.Serializable]
    public class AlreadyDefinedOparationNameException : Exception
    {

        public AlreadyDefinedOparationNameException() { }
        public AlreadyDefinedOparationNameException(string message) : base(message) { }
        public AlreadyDefinedOparationNameException(string message, Exception inner) : base(message, inner) { }
        protected AlreadyDefinedOparationNameException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
