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
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),

};

var conanCards = new List<MagicCardBase>
{
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),
    new HealingCard(Chance.Always, Power.FromValue(2f)),

};

var battleStrategyFactory = app.Services.GetService<IBattleStrategyFactory>();
var visitor = new Visitor();
for (int i = 0; i < 1; i++)
{
    var conan = Warrior.Create("Conan", SphereBase.BlueSky, Level.FromNumber(1), conanCards);
    var brutus = Warrior.Create("Brutus", SphereBase.BlueSky, Level.FromNumber(1), brutusCards);

    var battleStrategy = battleStrategyFactory!.SelectBy(conan.CurrentSphere);

    BattleResult battleResult = battleStrategy.StartBattle(new BattleContext(conan, brutus, 20));
    
    foreach (var e in battleResult.BattleEvents)
    {
        e.Accept(visitor);
    }
}

Console.WriteLine("Game over");


