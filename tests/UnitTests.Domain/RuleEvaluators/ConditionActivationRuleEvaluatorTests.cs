using System;
using System.Collections.Generic;
using System.Text;
using Domain.ActivationRules;
using Domain.BattlePlans;
using Domain.MagicCards.Rules;
using Domain.Shared;
using Shouldly;

namespace Domain.UnitTests.RuleEvaluators;

public sealed class ConditionActivationRuleEvaluatorTests
{
    [Fact]
    public void Matches_HealthRule_SholdReturnTrue()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 1, []);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3, []);

        var evaluator = new ConditionActivationRuleEvaluator();

        var ctx = new AttackContext(conan, brutus);
        evaluator.Matches(new ConditionActivationRule((ctx) => { return ctx.Attacker.Health == 100; }), ctx).ShouldBeTrue();
    }

    [Fact]
    public void Matches_HealthRule_SholdReturnFalse()
    {
        var conan = WarriorHelper.CreateBlueSky("Connan", 1, []);
        var brutus = WarriorHelper.CreateBlueSky("Brutus", 3, []);

        var evaluator = new ConditionActivationRuleEvaluator();

        var ctx = new AttackContext(conan, brutus);
        evaluator.Matches(new ConditionActivationRule((ctx) => { return ctx.Attacker.Health == 50; }), ctx).ShouldBeFalse();
    }
}
