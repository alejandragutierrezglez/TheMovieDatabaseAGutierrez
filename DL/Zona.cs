﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class Zona
{
    public int IdZona { get; set; }

    public string? Nombre { get; set; }
    public decimal VENTA_POR_ZONA { get; set; }

    public virtual ICollection<Cine> Cines { get; } = new List<Cine>();
}
