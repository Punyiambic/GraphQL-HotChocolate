using System;
using System.Collections.Generic;

namespace Product.Data.Entities;

public partial class Brand
{
    public int BrandId { get; set; }

    public Guid BrandUid { get; set; }

    public string Name { get; set; } = null!;

    public int StatusId { get; set; }

    public virtual ICollection<LobTeam> LobTeams { get; set; }

    public virtual ICollection<LobTeamBrand> LobTeamBrands { get; set; } = new List<LobTeamBrand>();
}
