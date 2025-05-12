using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WlChallenge.Domain.Entities;

namespace WlChallenge.Infra.Data.Maps;

public class WalletMap : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("wallets");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.Balance)
            .HasColumnName("balance");


        builder.HasMany(x => x.Transactions)
            .WithOne(x => x.Wallet)
            .HasForeignKey(x => x.WalletId);

        builder.Navigation(x => x.Transactions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsOne(x => x.Tracker)
            .Property(x => x.CreatedAtUtc)
            .HasColumnName("created_at");
        builder.OwnsOne(x => x.Tracker)
            .Property(x => x.UpdatedAtUtc)
            .HasColumnName("updated_at");
    }
}