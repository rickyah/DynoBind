using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper
{
    /// <summary>
    /// Provides an interface for performing common indexer operations over a type
    /// </summary>
    public interface IIndexerCall 
    {
        /// <summary>
        /// Performs indexer access over a type.
        /// </summary>
        /// <param name="indexList">object used as indexer</param>
        /// <returns>
        /// An <see cref="IGetSetInvoker"/> instance that will establish the operation to
        /// perform over the specifyed index.
        /// </returns>
        IGetSetOperations Index(params object[] indexList);
    }
}
