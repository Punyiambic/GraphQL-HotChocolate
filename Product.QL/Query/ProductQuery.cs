using Product.Data.Context;
using Product.Data.Entities;
using Microsoft.EntityFrameworkCore;
using UseFiltering = HotChocolate.Data.UseFilteringAttribute;
using UseSorting = HotChocolate.Data.UseSortingAttribute;

namespace Product.QL.Query
{
    public class ProductQuery
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Brand> Brands(ProductDataContext context) => context.Brands.AsNoTracking();

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<LobTeam> LobTeams(ProductDataContext context) => context.LobTeams.AsNoTracking();
    }
}