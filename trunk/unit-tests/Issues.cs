using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

namespace LateBindingHelper.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class IssuesTestFixture
    {
        #region Test Members

        IDynamic _lateBindingFacade;

        #endregion

        [SetUp]
        public void SetUp()
        {
            _lateBindingFacade = BindingFactory.CreateObjectBinding(typeof(MyLateBindingTestType), 27);
        }


        [Test]
        public void Issue3_RecoveringFromBadCall()
        {

            try
            {        
                _lateBindingFacade.Method("SimpelMethod").Invoke();
            }
            catch { }
            _lateBindingFacade.Method("SimpleMethod").Invoke();

            try
            {
                _lateBindingFacade.Property("MyProper").Get<int>();
            }
            catch { }
            _lateBindingFacade.Property("MyProp").Get<int>();


            try
            {
                _lateBindingFacade.Field("myFieldd").Get<int>();
            }
            catch { }
            _lateBindingFacade.Field("myField").Get<int>();
        }

    }
}
