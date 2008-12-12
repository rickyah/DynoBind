using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper
{
    /// <summary>
    /// Defines operations to add parameters to a method call and mantaining a list with they
    /// </summary>
    public interface IParameterBuilder
    {

        /// <summary>
        /// Adds a parameter to the list and mark it as passed with reference semantics
        /// </summary>
        /// <param name="value">object to be passed as parameter</param>
        /// <returns>
        /// A reference to the object which made this operation
        /// </returns>
        IParameterBuilder AddRefParameter(object value);

        /// <summary>
        /// Adds a parameter to the list and mark it as passed with value semantics
        /// </summary>
        /// <param name="value">object to be passed as parameter</param>
        /// <returns>
        /// A reference to the object which made this operation
        /// </returns>
        IParameterBuilder AddParameter(object value);

        /// <summary>
        /// Returns a read-only array of bools with the same length of the current parameters
        /// count saved by this object. Each position in the array determines if the
        /// parameter with the same position is marked as passed wit reference semantics
        /// (<c>true</c>) or with value semantics (<c>false</c>)
        /// </summary>
        IList<bool> GetReferenceParameterList();

        /// <summary>
        /// Gets or sets an array with objects as the parameters.
        /// </summary>
        object[] AsArray { get; set;}

        /// <summary>
        /// Returns the total parameter count.
        /// </summary>
        int Count { get;}


        /// <summary>
        /// Discards all saved parameters
        /// </summary>
        void Clear();

        /// <summary>
        /// Access the parameter at the specified index.
        /// </summary>
        object this[int index ]{ get;}
    }
}