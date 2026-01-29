using System;
using System.Collections.Generic;
using System.Text;
using Domain.Shared;

namespace Domain.Warriors.Events;

public sealed record CardStolen(string CardName) : DomainEventBase;

