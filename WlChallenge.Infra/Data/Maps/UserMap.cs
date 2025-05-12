using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WlChallenge.Domain.Entities;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Infra.Data.Maps;

public sealed class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(User.MaxNameLength);

        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasMaxLength(150)
            .HasColumnName("email");

        builder.OwnsOne(x => x.Password)
            .Property(x => x.HashText)
            .HasColumnName("password")
            .HasColumnType("varchar(70)");

        builder.OwnsOne(x => x.Document)
            .Property(x => x.Number)
            .HasColumnName("document_number")
            .HasColumnType($"varchar({Cnpj.MinLength})");
        builder.OwnsOne(x => x.Document)
            .Property(x => x.Type)
            .HasColumnName("document_type");

        builder.HasOne(x => x.Wallet)
            .WithOne(x => x.User)
            .HasForeignKey<Wallet>(x => x.UserId);

        builder.OwnsOne(x => x.Tracker)
            .Property(x => x.CreatedAtUtc)
            .HasColumnName("created_at");
        builder.OwnsOne(x => x.Tracker)
            .Property(x => x.UpdatedAtUtc)
            .HasColumnName("updated_at");
    }
}