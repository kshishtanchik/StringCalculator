using System;
using System.Collections.Generic;
using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringCalcTest
{
    [TestClass]
    public class UnitTest1
    {
        class InputParam
        {
            public decimal ExprctedValue { get; set; }
            public string ActualInput { get; set; }
        }
        [TestMethod]
        public void TestMethod1()
        {
            var inputStrings = new List<InputParam>
            {
               new InputParam() {ExprctedValue=15,ActualInput="10+5" },
               new InputParam()  {ExprctedValue=5,ActualInput="10-5" },
               new InputParam()  {ExprctedValue=20,ActualInput="5*4" },
               new InputParam()  {ExprctedValue=10 ,ActualInput="2,5*4"},
               new InputParam()  {ExprctedValue=1,ActualInput="9/9"},
               new InputParam()  {ExprctedValue=5,ActualInput="5-(10-2*5)"},
               new InputParam()  {ExprctedValue=5,ActualInput="5(10-2*5)"},
            };
            foreach (var inputString in inputStrings)
            {
                var testResult = new StringCalc(inputString.ActualInput);
                Assert.AreEqual(inputString.ExprctedValue, testResult.MathResult);
            }
            
        }
    }
}
