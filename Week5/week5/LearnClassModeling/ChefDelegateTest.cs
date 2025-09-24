using System.Diagnostics;

namespace LearnClassModeling;

[TestClass]
public class ChefDelegateTest
{
    [TestMethod]
    public void ReviewDelegate()
    {
        Func<Ingredient, Dish> chef = Cook;

        Dish aDish = GetRandomDish(chef);
        Dish veggieDish = chef.Invoke(new Veg());

        Assert.IsInstanceOfType <VegDelight>(veggieDish);
    }

    [TestMethod]
    public void TestInterfaceAsMethodParameters()
    {
        ProgramAgainstInterface(new MP3Player());
        ProgramAgainstInterface(new Car());
    }


    private Dish GetRandomDish(Func<Ingredient,Dish> chef)
    {
        Ingredient[] ingredients = [new Fish(), new Veg(), new Beef()];
        return chef.Invoke(ingredients[Random.Shared.Next(3)]);
    }

    private Dish Cook(Ingredient ingredient) =>

        ingredient switch
        {
            Fish => new FishFry(),
            Beef => new MongolianBeef(),
            Veg => new VegDelight(),
            _ => new PoBoy()

        };
    

    private void ProgramAgainstInterface(IPlayer player)
    {
        player.Next();
    }
}

public class Ingredient { }
public class Fish : Ingredient;
public class Veg : Ingredient;
public class Beef : Ingredient;

public class Dish { }
public class FishFry : Dish { }
public class VegDelight : Dish { }
public class MongolianBeef : Dish { }
public class PoBoy : Dish { }


 interface IPlayer
{
    void Play();
    void Pause();
    void Stop();
    void Next();
    void Prev();
}
public class Car : IPlayer
{
    public void Next()
    {
        Debug.WriteLine("Car blasting stereo");
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Play()
    {
        throw new NotImplementedException();
    }

    public void Prev()
    {
        throw new NotImplementedException();
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }
}
public class Player : IPlayer
{
    public virtual void Next()
    {
        throw new NotImplementedException();
    }

    public virtual void Pause()
    {
        throw new NotImplementedException();
    }

    public virtual void Play()
    {
        throw new NotImplementedException();
    }

    public virtual void Prev()
    {
        throw new NotImplementedException();
    }

    public virtual void Stop()
    {
        throw new NotImplementedException();
    }
}
public class MP3Player : Player
{
    public override void Next()
    {
        Debug.WriteLine("MPS playing next song.");
    }
}
public class IPodPlayer : Player;
public class CDPlayer : Player;
public class YouTubePlayer : Player;

