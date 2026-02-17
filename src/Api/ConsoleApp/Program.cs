using ConsoleApp;
using Domain.ActivationRules;
using Domain.BattlePlans;
using Domain.Battles;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.MagicCards.Cards;
using Domain.Warriors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

Domain.DependencyInjection.AddDomainServices(builder.Services);
DependencyInjection.AddProgramServices(builder.Services);

var app = builder.Build();


var brutusSlots = new List<Slot>
{
    new Slot(
         HealingCard.Create(Power.FromValue(10 )),
        new ConditionActivationRule(ctx => ctx.Attacker.Health < 30),
        0),
    new Slot(
         new FightingCard(Power.FromValue(10)),
        new ConditionActivationRule(ctx => ctx.Opponent.Health > 100),
        0),
    new Slot(
         new FightingCard(Power.FromValue(5)),
        new ChanceActivationRule(Chance.CoinFlip),
        0),
};

var conanCards = new List<Slot>
{
    new Slot(
         HealingCard.Create(Power.FromValue(10)),
        new ConditionActivationRule(ctx => ctx.Attacker.Health < 30),
        0),
    new Slot(
         HealingCard.Create(Power.FromValue(20)),
        new ConditionActivationRule(ctx => ctx.Attacker.Health < 10),
        1),
    new Slot(
         HealingCard.Create(Power.FromValue(10)),
        new ConditionActivationRule(ctx => ctx.Attacker.Health < 50),
        2),
};

var battleStrategyFactory = app.Services.GetService<IBattleStrategyFactory>();
var judge = new Judge();
for (int i = 0; i < 1; i++)
{
    var conan = Warrior.Create(WarriorId.New(), "Conan", SphereBase.BlueSky, Level.FromNumber(1), conanCards);
    var brutus = Warrior.Create(WarriorId.New(), "Brutus", SphereBase.BlueSky, Level.FromNumber(1), brutusSlots);

    var battleStrategy = battleStrategyFactory!.SelectBy(conan.CurrentSphere);

    BattleResult battleResult = battleStrategy.StartBattle(BattleContext.Create(conan, brutus, 100), DateTimeOffset.Now);

    judge.MakeReport(battleResult.BattleEvents);
}

Console.WriteLine("Game over");


