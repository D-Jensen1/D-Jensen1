namespace LearnMethods;

[TestClass]
public class ReturnsTest
{
    [TestMethod]
    public void SingleValueReturn()
    {
        Assert.AreEqual(2,Add(1,1));
    }

    [TestMethod]
    public void ArrayValueReturnTest()
    {
        int[] result = ToArray(1, 2);
        Assert.AreEqual(1, result[0]);
        Assert.AreEqual(2, result[1]);

    }

    [TestMethod]
    public void TupleReturnResult()
    {
        var result = ReturnTuple(1, 2);
        Assert.AreEqual(1, result.Item1);
        Assert.AreEqual(2, result.Item2);
        Assert.AreEqual(DateTime.Today, result.Item3);
        Assert.AreEqual("Bob", result.Item4);

    }

    [TestMethod]
    public void TupleInputResult()
    {
        var result = InputTuple(1, 2, ("Bob", DateTime.Today));
        Assert.AreEqual(1, result.Item1);
        Assert.AreEqual(2, result.Item2);
        Assert.AreEqual(DateTime.Today, result.Item3);
        Assert.AreEqual("Bob", result.Item4);

    }
    public int Add(int x, int y)
    {
        return x + y;
    }
    
    public int[] ToArray(int x, int y)
    {
        return new int[] { x, y };
    }

    public (int,int, DateTime, string) ReturnTuple(int x, int y)
    {
        return (x, y, DateTime.Today, "Bob");
        // returning tuple reduces the need to create DTO (data transfer object) type
    }

    public (int, int, DateTime, string) InputTuple(int x, int y,(string,DateTime) inputTuple)
    {
        return (x, y, inputTuple.Item2, inputTuple.Item1);
        // returning tuple reduces the need to create DTO (data transfer object) type
    }
}
