﻿// <auto-generated />
using System;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    [DbContext(typeof(RestaurantManagementContext))]
    partial class RestaurantManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.LoggingInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(250)")
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(250);

                    b.Property<string>("ControllerName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(250)")
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(250);

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("('')");

                    b.Property<DateTime?>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("('')");

                    b.HasKey("Id");

                    b.ToTable("LoggingInfo");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblCuisine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cuisine")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(225)")
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(225);

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.ToTable("tblCuisine");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal?>("X")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<decimal?>("Y")
                        .HasColumnType("decimal(18, 0)");

                    b.HasKey("Id");

                    b.HasIndex("X")
                        .IsUnique()
                        .HasName("UQ__tblLocat__3BD0198414754610")
                        .HasFilter("[X] IS NOT NULL");

                    b.HasIndex("Y")
                        .IsUnique()
                        .HasName("UQ__tblLocat__3BD01987EC697B94")
                        .HasFilter("[Y] IS NOT NULL");

                    b.ToTable("tblLocation");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Item")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(225)")
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(225);

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("TblCuisineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblCuisineID")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Quantity")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.HasIndex("TblCuisineId");

                    b.ToTable("tblMenu");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Discount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("FromDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("TblMenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblMenuID")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("TblRestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblRestaurantID")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("ToDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.HasIndex("TblMenuId");

                    b.HasIndex("TblRestaurantId");

                    b.ToTable("tblOffer");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("('')");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(10)")
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(10);

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("TblCustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblCustomerId")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("TblRestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblRestaurantID")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.HasIndex("TblRestaurantId");

                    b.ToTable("tblRating");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRestaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<string>("CloseTime")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(5)")
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(5);

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(225)")
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(225);

                    b.Property<string>("OpeningTime")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(5)")
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(5);

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("TblLocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblLocationID")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Website")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(225)")
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(225);

                    b.HasKey("Id");

                    b.HasIndex("TblLocationId");

                    b.ToTable("tblRestaurant");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRestaurantDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("TableCapacity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("TableCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("TblRestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblRestaurantID")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.HasIndex("TblRestaurantId");

                    b.ToTable("tblRestaurantDetails");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblMenu", b =>
                {
                    b.HasOne("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblCuisine", "TblCuisine")
                        .WithMany("TblMenu")
                        .HasForeignKey("TblCuisineId")
                        .HasConstraintName("FK_tblMenu_tblCuisineID")
                        .IsRequired();
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblOffer", b =>
                {
                    b.HasOne("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblMenu", "TblMenu")
                        .WithMany("TblOffer")
                        .HasForeignKey("TblMenuId")
                        .HasConstraintName("FK_tblOffer_tblMenuID")
                        .IsRequired();

                    b.HasOne("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRestaurant", "TblRestaurant")
                        .WithMany("TblOffer")
                        .HasForeignKey("TblRestaurantId")
                        .HasConstraintName("FK_tblOffer_tblRestaurantID")
                        .IsRequired();
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRating", b =>
                {
                    b.HasOne("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRestaurant", "TblRestaurant")
                        .WithMany("TblRating")
                        .HasForeignKey("TblRestaurantId")
                        .HasConstraintName("FK_tblRating_tblRestaurantID")
                        .IsRequired();
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRestaurant", b =>
                {
                    b.HasOne("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblLocation", "TblLocation")
                        .WithMany("TblRestaurant")
                        .HasForeignKey("TblLocationId")
                        .HasConstraintName("FK_tblRestaurant_tblLocationID")
                        .IsRequired();
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRestaurantDetails", b =>
                {
                    b.HasOne("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRestaurant", "TblRestaurant")
                        .WithMany("TblRestaurantDetails")
                        .HasForeignKey("TblRestaurantId")
                        .HasConstraintName("FK_tblRestaurantDetails_tblRestaurantID")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
