using System;
using System.Collections.Generic;
using System.Text;
using Domain.Shared;

namespace Domain.Warriors.Events;

public sealed record CardStolen(string CardName) : DomainEventBase;
public sealed record DamageBoosted(int DamageBeforeBoost, float BoostPower) : DomainEventBase;
public sealed record WarriorHealed(int OldValue, int NewValue) : DomainEventBase;
public sealed record TargetCouirsed(int CoursePower) : DomainEventBase;
