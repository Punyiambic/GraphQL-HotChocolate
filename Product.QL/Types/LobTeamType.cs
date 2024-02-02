using Product.Data.Entities;
using Product.QL.Filters;

namespace Product.QL.Types
{
    public sealed class LobTeamType : ObjectType<LobTeam>
    {
        protected override void Configure(IObjectTypeDescriptor<LobTeam> descriptor)
        {
            descriptor
                .Field(t => t.LobTeamBrands)
                .Ignore();
        }
    }
}
