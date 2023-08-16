public class Espresso
{
    
    public Espresso(string name)
    {
        Name = name;
        Console.WriteLine($"Making {Name}");
        Thread.Sleep( 1000 );
    }
    public virtual string Name { get; set; }

    public virtual string MakeCoffee()
    {
        return "Making esspresso";
    }
}
