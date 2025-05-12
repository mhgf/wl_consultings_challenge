using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WlChallenge.Domain.Entities;

namespace WlChallenge.Infra.Data.Maps;

public class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.Amount)
            .HasColumnName("amount");

        builder.Property(x => x.Type)
            .HasColumnName("type");

        builder.Property(x => x.SenderId)
            .HasColumnName("sender_id");

        builder.Property(x => x.ReceiverId)
            .HasColumnName("receiver_id");

        builder.Property(x => x.WalletId)
            .HasColumnName("wallet_id");

        builder.HasOne(x => x.Sender)
            .WithMany()
            .HasForeignKey(x => x.SenderId);

        builder.HasOne(x => x.Receiver)
            .WithMany()
            .HasForeignKey(x => x.ReceiverId);

        builder.HasOne(x => x.Wallet)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.WalletId);

        builder.OwnsOne(x => x.Tracker)
            .Property(x => x.CreatedAtUtc)
            .HasColumnName("created_at");
        builder.OwnsOne(x => x.Tracker)
            .Property(x => x.UpdatedAtUtc)
            .HasColumnName("updated_at");
    }
}