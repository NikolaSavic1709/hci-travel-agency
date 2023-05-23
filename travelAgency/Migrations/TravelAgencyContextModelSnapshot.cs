﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using travelAgency;

#nullable disable

namespace travelAgency.Migrations
{
    [DbContext(typeof(TravelAgencyContext))]
    partial class TravelAgencyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("travelAgency.model.Journey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Journeys");
                });

            modelBuilder.Entity("travelAgency.model.JourneyPlace", b =>
                {
                    b.Property<int>("JourneyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlaceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("JourneyId", "PlaceId");

                    b.HasIndex("PlaceId");

                    b.ToTable("JourneyPlace");
                });

            modelBuilder.Entity("travelAgency.model.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("travelAgency.model.JourneyPlace", b =>
                {
                    b.HasOne("travelAgency.model.Journey", "Journey")
                        .WithMany("JourneyPlaces")
                        .HasForeignKey("JourneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("travelAgency.model.Place", "Place")
                        .WithMany("JourneyPlaces")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Journey");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("travelAgency.model.Journey", b =>
                {
                    b.Navigation("JourneyPlaces");
                });

            modelBuilder.Entity("travelAgency.model.Place", b =>
                {
                    b.Navigation("JourneyPlaces");
                });
#pragma warning restore 612, 618
        }
    }
}
