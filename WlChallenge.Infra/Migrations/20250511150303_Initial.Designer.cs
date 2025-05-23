﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WlChallenge.Infra.Data;

#nullable disable

namespace WlChallenge.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250511150303_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WlChallenge.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint")
                        .HasColumnName("amount");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uuid")
                        .HasColumnName("receiver_id");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid")
                        .HasColumnName("sender_id");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uuid")
                        .HasColumnName("wallet_id");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.HasIndex("WalletId");

                    b.ToTable("transactions", (string)null);
                });

            modelBuilder.Entity("WlChallenge.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("WlChallenge.Domain.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Balance")
                        .HasColumnType("integer")
                        .HasColumnName("balance");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("wallets", (string)null);
                });

            modelBuilder.Entity("WlChallenge.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("WlChallenge.Domain.Entities.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WlChallenge.Domain.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WlChallenge.Domain.Entities.Wallet", "Wallet")
                        .WithMany("Transactions")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WlChallenge.Domain.ValueObjects.Tracker", "Tracker", b1 =>
                        {
                            b1.Property<Guid>("TransactionId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("CreatedAtUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("created_at");

                            b1.Property<DateTime>("UpdatedAtUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("updated_at");

                            b1.HasKey("TransactionId");

                            b1.ToTable("transactions");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");
                        });

                    b.Navigation("Receiver");

                    b.Navigation("Sender");

                    b.Navigation("Tracker")
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("WlChallenge.Domain.Entities.User", b =>
                {
                    b.OwnsOne("WlChallenge.Domain.ValueObjects.Tracker", "Tracker", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("CreatedAtUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("created_at");

                            b1.Property<DateTime>("UpdatedAtUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("updated_at");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("WlChallenge.Domain.ValueObjects.Document", "Document", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("varchar(14)")
                                .HasColumnName("document_number");

                            b1.Property<int>("Type")
                                .HasColumnType("integer")
                                .HasColumnName("document_type");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("WlChallenge.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("character varying(150)")
                                .HasColumnName("email");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("WlChallenge.Domain.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("HashText")
                                .IsRequired()
                                .HasColumnType("varchar(70)")
                                .HasColumnName("password");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Document")
                        .IsRequired();

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();

                    b.Navigation("Tracker")
                        .IsRequired();
                });

            modelBuilder.Entity("WlChallenge.Domain.Entities.Wallet", b =>
                {
                    b.HasOne("WlChallenge.Domain.Entities.User", "User")
                        .WithOne("Wallet")
                        .HasForeignKey("WlChallenge.Domain.Entities.Wallet", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WlChallenge.Domain.ValueObjects.Tracker", "Tracker", b1 =>
                        {
                            b1.Property<Guid>("WalletId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("CreatedAtUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("created_at");

                            b1.Property<DateTime>("UpdatedAtUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("updated_at");

                            b1.HasKey("WalletId");

                            b1.ToTable("wallets");

                            b1.WithOwner()
                                .HasForeignKey("WalletId");
                        });

                    b.Navigation("Tracker")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WlChallenge.Domain.Entities.User", b =>
                {
                    b.Navigation("Wallet")
                        .IsRequired();
                });

            modelBuilder.Entity("WlChallenge.Domain.Entities.Wallet", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
