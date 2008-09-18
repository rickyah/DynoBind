using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

using LateBindingHelper;
using LateBindingHelper.Tests;
using System.Reflection;

namespace LateBindingHelper.UnitTests
{
    [TestFixture]
    public class LateBindingHelperTestFixture
    {
        #region Test Members

        ILateBindingFacade _lateBindingFacade;

        #endregion

        #region TestFixture SetUp/TearDown

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            //TODO: Add test fixture set up code
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            //TODO: Add test fixture tear down code
        }

        #endregion
            
        #region  Test SetUp/TearDown

        [SetUp]
        public void SetUp()
        {
            _lateBindingFacade = LateBindingFactory.CreateBinding(typeof(MyLateBindingTestType), Args.Build(27) );
        }

        [TearDown]
        public void TearDown()
        {
            //TODO: Add test tear down code
        }

        #endregion

        #region Tests

        [Test]
        public void TestFieldAccess()
        {
            Assert.That(_lateBindingFacade.Get<Int32>("myField"), Is.EqualTo(27));

            _lateBindingFacade.Set("myField", -50);

            Assert.That(_lateBindingFacade.Get<Int32>("myField"), Is.EqualTo(-50));
        }

        [Test]
        public void TestSimpleMethodCall()
        {
            _lateBindingFacade.Call("SimpleMethod");
        }

        [Test]
        [ExpectedException(typeof (Exception) )]
        public void TestMethodCall_Failure()
        {
            _lateBindingFacade.Call("blerz");
        }

        [Test]
        public void TestMethodCall()
        {
            int sum = _lateBindingFacade.Call<int>("Sum", Args.Build(15, 17));

            Assert.That(sum, Is.EqualTo(15 + 17));
        }

        [Test]
        public void TestMethodWithReferenceValues()
        {
            int refValue = 10;
            object[] args = Args.Build(refValue);

            _lateBindingFacade.Call("MulFiveRef", Args.ByRefIndexs(0), args);

            refValue = (int)args[0];

            Assert.That(refValue, Is.EqualTo(10 * 5));
        }

        [Test]
        public void TestMethodWithMultipleReferenceValues()
        {
            int refValue1 = 10;
            int refValue2 = 20;
            object[] args = Args.Build(refValue1, refValue2);

            _lateBindingFacade.Call("MulTenRef", Args.ByRefIndexs(0), args);

            refValue1 = (int)args[0];
            refValue2 = (int)args[1];

            Assert.That(refValue1, Is.EqualTo(10 * 10));
            Assert.That(refValue2, Is.EqualTo(20 * 10));
        }

        [Test]
        public void TestPropertiesAccess()
        {
            Assert.That(_lateBindingFacade.Get<Int32>("MyProp"), Is.EqualTo(27));

            _lateBindingFacade.Set("MyProp", 69);
            Assert.That(_lateBindingFacade.Get<Int32>("MyProp"), Is.EqualTo(69));
            Assert.That(_lateBindingFacade.Get("MyProp"), Is.EqualTo(69));
        }

        [Test]
        public void TestIndexerAccess()
        {
            Assert.That(_lateBindingFacade.GetIndex<string>(5), Is.EqualTo( "default" ));

            _lateBindingFacade.SetIndex(4, "[myValue]");
            
            Assert.That(_lateBindingFacade.GetIndex<string>(5), Is.EqualTo( ("value:" + "[myValue]" ) ));
        }

        [Test]
        public void WordAutomationTest_NotReallyATestUnit()
        {
            ILateBindingFacade word = LateBindingFactory.CrateAutomationBinding("Word.Application");
            

            ILateBindingFacade wordDoc = word.Get<ILateBindingFacade>("Documents").Call<ILateBindingFacade>("Add");

            ILateBindingFacade selection = word.Get<ILateBindingFacade>("Selection");

            string str = "Hello World!";

            word.Set("Visible", true);

            foreach (char c in str)
            {
                selection.Call("TypeText", Args.Build(c.ToString()));
                System.Threading.Thread.Sleep(200);
            }

            //Needed to get the type of the enumeration 
            Type enumType = Assembly.LoadWithPartialName("Microsoft.Office.Interop.Word").GetType("Microsoft.Office.Interop.Word.WdSaveOptions", false);

            word.Call("Quit", Args.Build(Activator.CreateInstance(enumType)));
        }
        #endregion
    }
}
