using System;
using System.Collections.Generic;

namespace Product.Data.Entities;

public partial class LobTeam
{
    public int LobTeamId { get; set; }

    public Guid LobTeamUid { get; set; }

    public string Name { get; set; } = null!;

    public int StatusId { get; set; }

    public string? LobTeamCode { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string LastModifiedBy { get; set; } = null!;

    public virtual ICollection<Brand> Brands { get; set; }

    public virtual ICollection<LobTeamBrand> LobTeamBrands { get; set; } = new List<LobTeamBrand>();
}
