﻿// <auto-generated />
using System;
using FlightBooking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightBooking.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230926144302_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("FlightBooking.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BookingDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("DestinationAirportCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceAirportCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("TravelDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("FlightBooking.Models.TripSearchResults", b =>
                {
                    b.Property<int>("TripSearchResultsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CachedTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Results")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TripSearchResultsId");

                    b.ToTable("SearchResults");
                });
#pragma warning restore 612, 618
        }
    }
}
