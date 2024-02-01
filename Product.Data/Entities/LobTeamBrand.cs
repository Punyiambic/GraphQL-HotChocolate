using System;
using System.Collections.Generic;

namespace Product.Data.Entities;

public partial class LobTeamBrand
{
    public int LobTeamBrandId { get; set; }

    public int LobTeamId { get; set; }

    public int BrandId { get; set; }

    public int StatusId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual LobTeam LobTeam { get; set; } = null!;
}
