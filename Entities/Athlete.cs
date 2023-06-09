﻿using System;
using System.Collections.Generic;

namespace CharityScanWebApp.Entities;

public partial class Athlete
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool Sex { get; set; }

    public int Age { get; set; }

    public virtual ICollection<Code> Codes { get; } = new List<Code>();

    public virtual ICollection<Volunteer> Volunteers { get; } = new List<Volunteer>();
}
