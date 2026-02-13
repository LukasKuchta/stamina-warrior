using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Contract.V1.Warriors.WarriorDetails;

public sealed record WarriorDetailsDto
{
    public Guid Id { get; set; }
}
