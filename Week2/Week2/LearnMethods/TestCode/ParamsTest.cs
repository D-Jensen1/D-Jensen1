using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;

namespace LearnMethods.TestCode
{
    [TestClass]
    public sealed class ParamsTest
    {
        [TestMethod]
        public void LocalFunctionTest()
        {
            int a = 1, b = 2;
            int actual = Add(a, b);
            Assert.AreEqual(3, actual);

            int Add(int x, int y)
            {
                return x + y;
            }

            actual = Subtract(b, a);
            Assert.AreEqual(1, actual);

            int Subtract(int x, int y) => x - y;
            // Add and Subtract can ONLY be seen in LocalFunctionTest
        }

        [TestMethod]
        public void RefParameterTest()
        {
            int a = 100;
            int expected = 100;
            ValueParam(a);
            Assert.AreEqual(100, a);

            ValueParamByRef(ref a);
            Assert.AreEqual(101, a);


            void ValueParam(int x)
            {
                x = x + 1;
            }
            void ValueParamByRef(ref int x)
            {
                x = x + 1;
            }


            float eurRate = default;
            GetExchangeRate(ref eurRate, "EUR");

            float gbpRate = default;
            GetExchangeRate(ref gbpRate, "GBP");

            void GetExchangeRate(ref float x, string currency)
            {
                switch (currency)
                {
                    case "USD":
                        x = 1F;
                        break;
                    case "EUR":
                        x = 1.2f;
                        break;
                    case "GBP":
                        x = 1.5f;
                        break;
                }
            }

        }

        [TestMethod]
        public void OutParameterTest()
        {
            string size = string.Empty;
            int waist = 10;
            Assert.IsFalse(TryParse(waist, out size));
            Assert.AreEqual(string.Empty, size);

            waist = 25;
            Assert.IsTrue(TryParse(waist, out size));
            Assert.AreEqual("Small", size);

            waist = 35;
            Assert.IsTrue(TryParse(waist, out size));
            Assert.AreEqual("Medium", size);

            waist = 45;
            Assert.IsTrue(TryParse(waist, out size));
            Assert.AreEqual("Large", size);

            waist = 55;
            Assert.IsFalse(TryParse(waist, out size));
            Assert.AreEqual(string.Empty, size);

            bool TryParse(int waist, out string size)
            {
                size = waist switch
                {
                    >= 20 and <= 30 => "Small",
                    >= 31 and <= 40 => "Medium",
                    >= 41 and <= 50 => "Large",
                    _ => string.Empty // Default
                };
                return size != string.Empty;
            }


        }

        [TestMethod]
        public void ParamsParameterTest()
        {
            Assert.AreEqual(15, AddAll(1, 2, 3, 4, 5));
            Assert.AreEqual(235, AddAll(45, 46, 47, 48, 49));
            Assert.AreEqual(6, AddAll([1, 2,3]));

            PrintAll("Bob", "Tom", "Joe", "Jim");


            int AddAll(params int[] numbers) 
            {
                int result = 0;
                foreach( var item in numbers)
                {
                    result += item;
                }
                return result;
            }

            void PrintAll(params List<string> names) // List<string> pronounced as List of Strings, stores a list of strings
            { // params keyword turns comma delimited input params into collection
                foreach (var item in names)
                {
                    Console.WriteLine(item);
                }
            }
        }

        [TestMethod] // Polymorphism
        public void ParamsOverLoadingTest()
        {
            PrintGreeting("Bob", 3);
            PrintGreeting("bob", "10");
        }
        void PrintGreeting(string name, int age) //two methods can have the same name, as long as the parameters differ
        {
            Console.WriteLine($"Hello {name}, age: {age}");
            }

        void PrintGreeting(string name, string age)
        {
            Console.WriteLine($"Hello {name}, age: {age}");
        }
    }

}

   
    //all method must be defined under a class, local functions can be defined inside of methods

