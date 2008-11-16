using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper
{
    public interface IParameterBuilder
    {
        IParameterBuilder AddRefParameter(object value);

        IParameterBuilder AddParameter(object value);

        object[] AsArray { get; set;}

        int Count { get;}

        void Clear();

        IList<bool> ISReferenceParameter { get;}

        object this[int index] { get;}
    }
}