namespace Domain.Battles.Events;

public sealed record WarrirorStat
{

    internal WarrirorStat(string name, int health, int maxDamage)
    {
        Name = name;
        Health = health;
        MaxDamage = maxDamage;
    }

    public string Name { get; }
    public int Health { get; }
    public int MaxDamage { get; }

    override public string ToString() => $"{Name} (Health: {Health}, MaxDamage: {MaxDamage})";
}
