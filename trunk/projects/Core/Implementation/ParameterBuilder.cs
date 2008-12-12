using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper.Implementation
{
    /// <summary>
    /// Simple implementation of a <see cref="IParameterBuilder"/>
    /// </summary>
    public class ParameterBuilder : IParameterBuilder
    {
        private List<object> _parameters = new List<object>();
        private List<bool> _isRef = new List<bool>();

        #region Miembros de IParameterList

        /// <summary>
        /// Adds a parameter to the list and mark it as passed with value semantics
        /// </summary>
        /// <param name="value">object to be passed as parameter</param>
        /// <returns>
        /// A reference to the object which made this operation
        /// </returns>
        public IParameterBuilder AddParameter(object value)
        {
            _parameters.Add(value);
            _isRef.Add(false);

            return this;
        }

        /// <summary>
        /// Adds a parameter to the list and mark it as passed with reference semantics
        /// </summary>
        /// <param name="value">object to be passed as parameter</param>
        /// <returns>
        /// A reference to the object which made this operation
        /// </returns>
        public IParameterBuilder AddRefParameter(object value)
        {

            _parameters.Add(value);
            _isRef.Add(true);

            return this;
        }

        /// <summary>
        /// Discards all saved parameters
        /// </summary>
        public void Clear()
        {
            _parameters.Clear();
            _isRef.Clear();
        }

        /// <summary>
        /// Gets or sets an array with objects as the parameters.
        /// </summary>
        /// <value></value>
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

        /// <summary>
        /// Access the parameter at the specified index.
        /// </summary>
        /// <value></value>
        public object this[int index]
        {
            get
            {
                return _parameters[index];
            }
        }

        /// <summary>
        /// Returns a read-only array of bools with the same length of the current parameters
        /// count saved by this object. Each position in the array determines if the
        /// parameter with the same position is marked as passed wit reference semantics
        /// (<c>true</c>) or with value semantics (<c>false</c>)
        /// </summary>
        /// <returns></returns>
        public IList<bool> GetReferenceParameterList()
        {
             return _isRef.AsReadOnly();
        }

        /// <summary>
        /// Returns the total parameter count.
        /// </summary>
        /// <value></value>
        public int Count
        {
            get { return _parameters.Count; }
        }
        #endregion
    }
}
