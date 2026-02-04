using System.Text;
using Domain.Battles;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.MagicCards.Cards;
using Domain.Warriors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StaminaWarrior;

var builder = Host.CreateApplicationBuilder(args);

Domain.DependencyInjection.AddDomainServices(builder.Services);
StaminaWarrior.DependencyInjection.AddProgramServices(builder.Services);

var app = builder.Build();


var brutusCards = new List<MagicCardBase>
{
    new FightingCard(Chance.CoinFlip, Power.FromValue(2f)),
    
    new HealingCard(Chance.CoinFlip, Power.FromValue(1f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new FightingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(3f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(4f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(5f)),
    new FightingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(6f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(7f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(8f)),
    new FightingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(9f)),
    new FightingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(10f)),
};

var conanCards = new List<MagicCardBase>
{
    new FightingCard(Chance.CoinFlip, Power.FromValue(2f)),

    new HealingCard(Chance.CoinFlip, Power.FromValue(1f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new FightingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(3f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(4f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(5f)),
    new FightingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(6f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(7f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(8f)),
    new FightingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(9f)),
    new FightingCard(Chance.CoinFlip, Power.FromValue(2f)),
    new HealingCard(Chance.CoinFlip, Power.FromValue(10f)),
};

var battleStrategyFactory = app.Services.GetService<IBattleStrategyFactory>();
var judge = new Judge();
for (int i = 0; i < 1; i++)
{
    var conan = Warrior.Create(WarriorId.New(), "Conan", SphereBase.BlueSky, Level.FromNumber(1), conanCards);
    var brutus = Warrior.Create(WarriorId.New(), "Brutus", SphereBase.BlueSky, Level.FromNumber(1), brutusCards);

    var battleStrategy = battleStrategyFactory!.SelectBy(conan.CurrentSphere);

    BattleResult battleResult = battleStrategy.StartBattle(BattleContext.Create(conan, brutus, 20));

    judge.MakeReport(battleResult.BattleEvents);
}

Console.WriteLine("Game over");


