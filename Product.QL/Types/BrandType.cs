using Product.Data.Entities;

namespace Product.QL.Types
{
    public sealed class BrandType : ObjectType<Brand>
    {
        protected override void Configure(IObjectTypeDescriptor<Brand> descriptor)
        {
            descriptor
                .Field(t => t.LobTeamBrands)
                .Ignore();

        }
    }  
}
