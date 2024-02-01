using Product.Data.Entities;

namespace Product.QL.Types
{
    public sealed class LobTeamType : ObjectTypeExtension<LobTeam>
    {
        protected override void Configure(IObjectTypeDescriptor<LobTeam> descriptor)
        {
            descriptor
                .Field(t => t.LobTeamBrands)
                .Ignore();

            //descriptor
            //    .Field(t => t.Brands)
            //    .UseFiltering();

        }
    }
}
