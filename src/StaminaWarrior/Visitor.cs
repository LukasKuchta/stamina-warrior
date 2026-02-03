using System;
using System.Collections.Generic;
using System.Text;
using Domain.Battles;
using Domain.Battles.Events;

namespace StaminaWarrior;

internal sealed class Visitor : IBattleEventVisitor
{
    public void Visit(RoundStarted e)
    {
        Console.WriteLine($"--- Round {e.Round} started ---");
    }

    public void Visit(DoubleKnockoutOccurred e)
    {
        Console.WriteLine($"Double knockout occurred between {e.Attacker} and {e.Oponent}!");
    }

    public void Visit(WarriorDied e)
    {
        Console.WriteLine($"{e.DeadMan} has died! {e.Survivor} is the winner!");
    }

    public void Visit(BattleFinished e)
    {
        Console.WriteLine($"Battle finished! Winner: {e.Winner}, Loser: {e.Looser}");
    }

    public void Visit(BattleFinishedTied e)
    {
        Console.WriteLine($"Battle finished in a tie between {e.Warrior1Name} and {e.Warrior2Name}!");
    }

    public void Visit(AttackLanded e)
    {
        Console.WriteLine($"{e.AttackerName} landed an attack on {e.OponentName} for {e.Damage} damage.");
    }

    public void Visit(CardDrawn e)
    {
        Console.WriteLine($"{e.CardHolder} drew card: {e.CardName}");
    }

    public void Visit(BattleStarted e)
    {
        Console.WriteLine($"Battle started between {e.Attacker} vs {e.Oponent}!");
    }
}
