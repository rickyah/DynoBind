using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;


namespace LateBindingHelper.Tests 
{
    /// <summary>
    /// Test type for checking Late Binding
    /// </summary>
    public class MyLateBindingTestType
    {
        private string _indexMock = "default";
        public Int32 myField;
        private Dictionary<int, string> _strLst = new Dictionary<int,string>();
        private int[,] _matrix = new int [10,10];

        /// <summary>
        /// Constructor
        /// </summary>
        public MyLateBindingTestType()
        {
            myField = 1;
        }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="value"></param>
        public MyLateBindingTestType(Int32 value)
        {
            MyProp = value;
        }

        /// <summary>
        /// A simple method with no parameters or return values.
        /// </summary>
        public void SimpleMethod()
        {
            Console.Write("I'm a simple method!");
        }

        /// <summary>
        /// Method with a ref parameters, multiplies it by five
        /// </summary>
        /// <param name="x"></param>
        public void MulFiveRef(ref Int32 x)
        {
            x *= 5;
        }

        /// <summary>
        /// Method with two ref parameters, multiplies both by ten
        /// </summary>
        public void MulTenRef(ref Int32 x, ref Int32 y)
        {
            x *= 10;
            y *= 10;
        }

        /// <summary>
        /// Method with two parameters and a return value.
        /// </summary>
        /// <param name="i1">The i1.</param>
        /// <param name="i2">The i2.</param>
        /// <returns></returns>
        public Int32 Sum(Int32 i1, Int32 i2)
        {
            return i1 + i2;
        }

        /// <summary>
        /// Property
        /// </summary>
        public Int32 MyProp
        {
            get { return myField; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("value", value, "value must be > 0");
                myField = value;
            }
        }

        public MyLateBindingTestType OtherProp
        {
            get { return new MyLateBindingTestType(); }
        }

        /// <summary>
        /// Indexer
        /// </summary>
        public string this[int index]
        {
            get { return _strLst[index]; }
            set { _strLst[index] = value; }
        }

        /// <summary>
        /// Two-parameter indexer
        /// </summary>
        public int this[int index1, int index2]
        {
            get { return _matrix[index1, index2]; }
            set { _matrix[index1, index2] = value; }
        }

        /// <summary>
        /// Object.ToString () override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return typeof(MyLateBindingTestType).GetType().ToString();
        }
    }

}
