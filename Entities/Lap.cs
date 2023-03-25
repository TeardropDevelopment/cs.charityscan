using System;
using System.Collections.Generic;

namespace CharityScanWebApp.Entities;

public partial class Lap
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int? StarterNr { get; set; }

    public string Value { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
