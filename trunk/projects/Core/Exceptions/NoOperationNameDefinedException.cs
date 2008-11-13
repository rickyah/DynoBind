using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper.Exceptions
{
    
    [global::System.Serializable]
    public class  NoOperationNameDefinedException : Exception
    {

        public  NoOperationNameDefinedException() { }
        public  NoOperationNameDefinedException(string message) : base(message) { }
        public  NoOperationNameDefinedException(string message, Exception inner) : base(message, inner) { }
        protected  NoOperationNameDefinedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
