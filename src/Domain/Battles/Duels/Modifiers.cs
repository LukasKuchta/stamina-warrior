namespace Domain.Battles.Duels;

internal sealed class Modifiers
{
    private readonly Dictionary<Type, ModBase> _mods = [];

    public void Add(ModBase mod)
    {
        _mods.Add(mod.GetType(), mod);
    }

    public T Get<T>(T @default)
        where T : ModBase
    {
        return _mods.TryGetValue(typeof(T), out var val)
            ? (T)val
            : @default;
    }
}

