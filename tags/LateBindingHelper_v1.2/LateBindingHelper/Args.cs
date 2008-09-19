using System;
using System.Collections.Generic;
using System.Text;

namespace LateBindingHelper
{
    /// <summary>
    /// Simplifies operations over arguments
    /// </summary>
    public static class Args
    {
        /// <summary>
        /// Given a list of integers and builds an array of <see cref="bool"/>
        /// with a length equal to the maximum integer value found.
        /// The positions in the list integer are then set to <c>true</c>
        /// in the created bool array, before returning it; 
        /// </summary>
        /// <remarks>
        /// Negative index are discarted;
        /// </remarks>
        /// <param name="args">List of index in a bool array to set the elements to true.</param>
        /// <returns>a bool array with the elements with an index contained in the
        /// argument array set to <c>true</c></returns>
        public static bool[] ByRefIndexs( params int[] args )
        {
            int maxIndex = 0;

            foreach (int index in args)
            {
                if (index > maxIndex)
                    maxIndex = index;
            }

            bool[] byRefIndexList = new bool[maxIndex+1];

            foreach (int index in args)
            {
                if (index < 0) continue;
             
                byRefIndexList[index] = true;
            }

            return byRefIndexList;
        }


        /// <summary>
        /// Helper method for building a list of objects[] with the ones passed as parameter.
        /// </summary>
        /// <param name="args">list of objects t</param>
        /// <returns></returns>
        public static object[] Build(params object[] args)
        {
            return args;
        }

    }
}
