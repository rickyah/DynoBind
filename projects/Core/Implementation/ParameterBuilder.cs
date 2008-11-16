using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper.Implementation
{
    public class ParameterBuilder : IParameterBuilder
    {
        private List<object> _parameters = new List<object>();
        private List<bool> _isRef = new List<bool>();
        #region Miembros de IParameterList

        public IParameterBuilder AddParameter(object value)
        {
            _parameters.Add(value);
            _isRef.Add(false);

            return this;
        }

        public IParameterBuilder AddRefParameter(object value)
        {

            _parameters.Add(value);
            _isRef.Add(true);

            return this;
        }

        public void Clear()
        {
            _parameters.Clear();
            _isRef.Clear();
        }
        public object[] AsArray
        {
            get
            { 
                return _parameters.ToArray(); 
            }

            set
            {
                for (int i = 0; i <value.Length; ++i)
                {
                    _parameters[i] = value[i];
                }
            }
        }

        public object this[int index]
        {
            get
            {
                return _parameters[index];
            }
        }

        public IList<bool> ISReferenceParameter
        {
            get { return _isRef.AsReadOnly();  }
        }

        public int Count
        {
            get { return _parameters.Count; }
        }
        #endregion
    }
}
