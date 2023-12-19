﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RealEstatePropertyListingPlatform.Infrastructure;

#nullable disable

namespace RealEstatePropertyListingPlatform.Infrastructure.Migrations
{
    [DbContext(typeof(RealEstatePropertyListingPlatformContext))]
    [Migration("20231219170947_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RealEstatePropertyListingPlatform.Domain.Entities.Listing", b =>
                {
                    b.Property<Guid>("ListingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ListingCreatorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Negotiable")
                        .HasColumnType("boolean");

                    b.Property<List<string>>("Photos")
                        .HasColumnType("text[]");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("ListingId");

                    b.ToTable("Listings");
                });

            modelBuilder.Entity("RealEstatePropertyListingPlatform.Domain.Entities.Property", b =>
                {
                    b.Property<Guid>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Floor")
                        .HasColumnType("integer");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NumberOfBathrooms")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfFloors")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfRooms")
                        .HasColumnType("integer");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PropertyType")
                        .HasColumnType("integer");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SquareFeet")
                        .HasColumnType("integer");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PropertyId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("RealEstatePropertyListingPlatform.Domain.Entities.Listing", b =>
                {
                    b.OwnsOne("RealEstatePropertyListingPlatform.Domain.Records.PriceInfo", "Price", b1 =>
                        {
                            b1.Property<Guid>("ListingId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Currency")
                                .HasColumnType("integer")
                                .HasColumnName("PriceCurrency");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("PriceValue");

                            b1.HasKey("ListingId");

                            b1.ToTable("Listings");

                            b1.WithOwner()
                                .HasForeignKey("ListingId");
                        });

                    b.Navigation("Price")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
