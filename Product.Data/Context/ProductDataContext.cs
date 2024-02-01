using Product.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Product.Data.Context;

public partial class ProductDataContext : DbContext
{
    public ProductDataContext(DbContextOptions<ProductDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<LobTeam> LobTeams { get; set; }

    public virtual DbSet<LobTeamBrand> LobTeamBrands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand", "product");

            entity.Property(e => e.BrandUid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(100);
        });


        modelBuilder.Entity<LobTeam>(entity =>
        {
            entity.ToTable("LobTeam", "product");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("('James Griffiths')");
            entity.Property(e => e.LastModifiedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("('James Griffiths')");
            entity.Property(e => e.LastModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LobTeamCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LobTeamUid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.HasMany(e => e.Brands)
                .WithMany(e => e.LobTeams)
                .UsingEntity<LobTeamBrand>(
                    l => l.HasOne<Brand>(e => e.Brand).WithMany(e => e.LobTeamBrands).HasForeignKey(e => e.BrandId),
                    r => r.HasOne<LobTeam>(e => e.LobTeam).WithMany(e => e.LobTeamBrands).HasForeignKey(e => e.LobTeamId),
                    j =>
                    {
                        j.HasKey("LobTeamId", "BrandId");
                        j.ToTable("LobTeamBrand");
                    });

        });

        modelBuilder.Entity<LobTeamBrand>(entity =>
        {
            entity.ToTable("LobTeamBrand", "product");

            entity.HasOne(d => d.Brand).WithMany(p => p.LobTeamBrands)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LobTeamBrand_BrandId");

            entity.HasOne(d => d.LobTeam).WithMany(p => p.LobTeamBrands)
                .HasForeignKey(d => d.LobTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LobTeamBrand_LobTeamId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
