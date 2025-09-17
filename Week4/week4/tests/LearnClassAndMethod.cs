using System.Collections;
using System.Diagnostics.Metrics;

namespace tests
{
    [TestClass]
    public sealed class LearnClassAndMethod
    {
        [TestMethod]
        public void InstanceOfClassHasOwnState()
        {
            int[] arrA = [1, 2, 3];
            int[] arrB = [1, 2, 3, 4];
            // there are 2 instances of int[]
            // each one has seperate sets of data (state)

            Assert.AreEqual(3, arrA.GetLength(0));
            Assert.AreEqual(4, arrB.GetLength(0));
            arrA[0] = 10;

            Assert.AreEqual(10, arrA[0]);
            Assert.AreEqual(1, arrB[0]);


        }

        // Learn System.IO.FileInfo
        [TestMethod]
        public void FileInfo()
        {
            //FileInfo is a blueprint, design
            FileInfo textFile, binaryFile; // the 2 variables will point to instances of the design
            textFile = new FileInfo("text.txt");// instantiate an object form class by calling constructor
                                                // - a method with the same name as the class
            binaryFile = new FileInfo("text.bin");

            Assert.IsFalse(textFile.Exists);
            Assert.IsFalse(binaryFile.Exists);

            var streamWriter = textFile.CreateText();
            streamWriter.Close();
            Assert.IsTrue(textFile.Exists);
            Assert.AreEqual(".txt", textFile.Extension);
            Assert.AreEqual(0, textFile.Length);
           
            textFile.Delete();
        }

        // List Class
        [TestMethod]
        public void WhatIsAList()
        {
            List<int> testList = [5, 10, 25, 30]; // the modern way
            //List<int> testList2 = new List<int>(testList); // the older way

            Assert.AreEqual(4, testList.Count);
            testList.Add(40);
            Assert.AreEqual(5, testList.Count);
            Assert.AreEqual(10, testList[1]);

            List<int> anotherList = [100, 200, 300, 400, 500];
            Assert.AreEqual(3, anotherList.IndexOf(400));
            Assert.AreEqual(1, anotherList.IndexOf(200));
            Assert.AreEqual(-1, anotherList.IndexOf(700));


        }

        // HashSet<T>
        [TestMethod]
        public void WhatIsAHashSet()
        {
            // instantiate a HashSet<int> using default constructor with no initialization
            HashSet<int> testSet = new HashSet<int>();

            // try to add 1,2,3,1,2
            testSet.Add(1);
            testSet.Add(2);
            testSet.Add(3);
            testSet.Add(1);
            testSet.Add(2);

            Assert.AreEqual(3, testSet.Count);

            HashSet<int> otherSet = [3, 4, 5];
            testSet.ExceptWith(otherSet);

            Assert.AreEqual(2, testSet.Count);

            testSet.Add(3); // put 3 back
            testSet.IntersectWith(otherSet);

            Assert.AreEqual(1, testSet.Count);
            Assert.IsTrue(testSet.Contains(3));

            testSet.Add(1);
            testSet.Add(2);

            testSet.UnionWith(otherSet);
            Assert.AreEqual(5, testSet.Count);
            Assert.IsTrue(testSet.SetEquals([1,2,3,4,5]));

        }

        // Stack<T>
        [TestMethod]
        public void WhatIsAStack()
        {
            Stack<int> testStack = new Stack<int>();
            testStack.Push(1);
            testStack.Push(2);
            testStack.Push(3);
            testStack.Push(4);

            Assert.AreEqual(4, testStack.Count);
            Assert.AreEqual(4, testStack.First());
            Assert.AreEqual(1, testStack.Last());


            Assert.AreEqual(4,testStack.Pop());
            Assert.AreEqual(3, testStack.Count);
            Assert.AreEqual(3, testStack.First());
            Assert.AreEqual(1, testStack.Last());


            testStack.Push(4);
            Assert.AreEqual(4, testStack.First());
            Assert.AreEqual(1, testStack.Last());

            Assert.AreEqual(4, testStack.Pop());
            Assert.AreEqual(3, testStack.Pop());
            Assert.AreEqual(2, testStack.Pop());
            Assert.AreEqual(1, testStack.Pop());

            Assert.AreEqual(0, testStack.Count);
            Assert.AreEqual(4, testStack.Capacity);

            testStack.Push(1);
            testStack.Push(2);
            testStack.Push(3);
            testStack.Push(4);
            testStack.Push(5);

            Assert.IsTrue(testStack.Contains(3));
            Assert.AreEqual(15, testStack.Sum());

            int[] intArray = testStack.ToArray();
            Assert.AreEqual(5, intArray[0]);

            Assert.AreEqual(5, testStack.Count);
            testStack.Clear();
            Assert.AreEqual(0, testStack.Count);
            Assert.AreEqual(8, testStack.Capacity); // capacity continues to double

            testStack = new([100, 200, 300]);
            Assert.AreEqual(300, testStack.Peek());
            Assert.AreEqual(300, testStack.First());
            Assert.AreEqual(300, testStack.Pop());

        }

        // Queue<T>
        [TestMethod]
        public void WhatIsAQueue()
        {
            Queue<int> testQueue = new([1, 2, 3, 4, 5]);

            Assert.AreEqual(1, testQueue.Dequeue());
            Assert.AreEqual(4, testQueue.Count());

            Assert.AreEqual(5, testQueue.EnsureCapacity(1));
            Assert.AreEqual(10, testQueue.EnsureCapacity(10));

            testQueue.Enqueue(1);
            Assert.AreEqual(1, testQueue.Last());
            Assert.AreEqual(2, testQueue.First());

            
            int result = 0;
            foreach (var item in testQueue)
            {
                result += item;
            }
            Assert.AreEqual(15, testQueue.Sum());
            Assert.AreEqual(15, result);

            testQueue = new([1, 2, 3, 4, 5, 10]);
            var numbers = testQueue.GetEnumerator();
            int[] resultArray = new int[6];
            int counter = 0;
            while(numbers.MoveNext())
            {
                resultArray[counter++] = numbers.Current;
            }
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 10 }, resultArray);
        }

        [TestMethod]
        public void test()
        {
            
        }
    }   
}
