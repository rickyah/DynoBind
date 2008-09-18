using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingFacade
{
    /// <summary>
    /// Modifier to 
    /// </summary>
    public class Ref
    {
        private object _refType;

        public Ref(ref object inner )
        {
            _refType = inner;
        }
       

        public override string ToString()
        {
            return InnerObject.ToString();
        }

        public override bool Equals(object obj)
        {
            return InnerObject.Equals(obj);
        }

        public override int GetHashCode()
        {
            return InnerObject.GetHashCode();
        }

        public object InnerObject
        {
            get
            {
                return  _refType;
            }
            set
            {
                    _refType = value;
            }
        }


    }
}
