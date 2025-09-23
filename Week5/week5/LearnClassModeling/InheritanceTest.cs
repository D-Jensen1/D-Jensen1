namespace LearnClassModeling;

[TestClass]
public class InheritanceTest
{
    [TestMethod]
    public void TestMethod1()
    {
        Person p = new Person();
        Assert.AreEqual("Person", p.TypeName);

        Employee e = new Employee();
        Assert.AreEqual("Employee", e.TypeName);

        PermEmployee e1 = new PermEmployee();
        Assert.AreEqual("Employee", e1.TypeName);

        TempEmployee e2 = new TempEmployee();
        Assert.AreEqual("Employee", e2.TypeName);

        Customer c = new Customer();
        Assert.AreEqual("Person", c.TypeName);
    }

    [TestMethod]
    public void ProtectedTest()
    {
        Person p = new Person();
        Assert.AreEqual("I am a Person", p.Name);

        Employee e = new Employee();
        Assert.AreEqual("I am an Employee", e.Name);
    }
}


public class Person
{
    // field == data - access modifier: public, internal, private - protected(can be called
    // by this class or derived class)
    protected string name = "I am a Person";
    public string Name => name;
    //public string TypeName = "Person";
    public virtual string TypeName => "Person";
}

public class Employee : Person
{
    protected int employeeID;
    public int EmployeeID => employeeID;

    public override string TypeName => "Employee";

    public Employee()
    {
        employeeID = new Random().Next(1000000000);
        this.name = "I am an Employee";
    }
}

public class PermEmployee : Employee
{
    //public string TypeName = "Employee";

}
public class TempEmployee : Employee
{
    //public string TypeName = "Employee";

}

public class Customer : Person
{
    //public string TypeName = "Customer";

}