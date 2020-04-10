using NUnit.Framework;
using System;
using Assert = NUnit.Framework.Assert;

namespace StringCalcKata
{
    public class Tests
    {
        private StringCalc calc;

        [SetUp]
        public void Setup()
        {
            calc = new StringCalc();
        }

        [Test]
        public void Add_returns_zero_for_null()
        {
            CheckAdd("", 0);

        }

        [Test]
        public void Add_returns_number_itself()
        {
            CheckAdd("5", 5);

        }

        [Test]
        public void Add_returns_sum_for_two_numbers()
        {
            CheckAdd("5,5", 10);

        }

        [Test]
        public void Add_returns_sum_for_several_numbers()
        {
            CheckAdd("5,5,3,1", 14);

        }

        [Test]
        public void Add_returns_sum_for_numbers_with_two_delimiters()
        {
            CheckAdd("1\n2,3", 6);

        }

        [Test]
        public void Add_returns_sum_with_custom_delitimer()
        {
            CheckAdd("//!!!\n1!!!2", 3);
        }

        [Test]
        public void Add_throws_exception_for_negative_numbers()
        {
            Assert.Throws<ArithmeticException>(() => calc.Add("//!!!\n-1!!!-2!!!5"))
                .Message.Contains("-1" + "-2");
        }

        [Test]
        public void Add_ignores_number_more_than_thousand()
        {
            CheckAdd("//#\n1#999#1000#1001", 2000);
        }

        [Test]
        public void Add_returns_sum_with_custom_delitimer_any_size()
        {
            CheckAdd("//[***]\n1***999***1000***1001", 2000);
        }

        [Test]
        public void Add_returns_sum_with_custom_delitimers_any_size()
        {
            CheckAdd("//[*][%]\n1*2%3", 6);
        }

        [Test]
        public void Add_returns_sum_with_custom_delitimers_any_size_any_quantity()
        {
            CheckAdd("//[*!>][=+=][|||][,,,]\n1*!>2|||3=+=4,,,1001", 10);
        }

        private void CheckAdd(string actual, double expected)
        {
            Assert.AreEqual(expected, calc.Add(actual));
        }

        [TearDown]
        public void TearDown()
        {
            calc = null;
        }
    }
}