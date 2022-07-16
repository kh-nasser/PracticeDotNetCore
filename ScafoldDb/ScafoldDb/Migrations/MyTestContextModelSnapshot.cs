﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScafoldDb.DataContext;

#nullable disable

namespace ScafoldDb.Migrations
{
    [DbContext(typeof(MyTestContext))]
    partial class MyTestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ScafoldDb.DAO.TblAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tblAccount");
                });

            modelBuilder.Entity("ScafoldDb.DAO.TblNotifyEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<Guid>("BatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<long>("FkTblAccountTblNotifyEvent")
                        .HasColumnType("bigint")
                        .HasColumnName("FK_tblAccount_tblNotifyEvent");

                    b.Property<long>("FkTblAccountTblNotifyEventReciever")
                        .HasColumnType("bigint")
                        .HasColumnName("FK_tblAccount_tblNotifyEvent_Reciever");

                    b.Property<byte>("FkTblNotifyEventTypeTblNotifyEvent")
                        .HasColumnType("tinyint")
                        .HasColumnName("FK_tblNotifyEventType_tblNotifyEvent");

                    b.HasKey("Id");

                    b.HasIndex("FkTblAccountTblNotifyEvent");

                    b.HasIndex("FkTblAccountTblNotifyEventReciever");

                    b.HasIndex(new[] { "BatchId" }, "IX_batchId");

                    b.HasIndex(new[] { "FkTblNotifyEventTypeTblNotifyEvent" }, "IX_tblNotifyEvent_FK_tblNotifyEventType_tblNotifyEvent");

                    b.ToTable("tblNotifyEvent", "notify");
                });

            modelBuilder.Entity("ScafoldDb.DAO.TblNotifyEventType", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("tblNotifyEventType", "notify");
                });

            modelBuilder.Entity("ScafoldDb.DAO.TblNotifyEvent", b =>
                {
                    b.HasOne("ScafoldDb.DAO.TblAccount", "FkTblAccountTblNotifyEventNavigation")
                        .WithMany("TblNotifyEventFkTblAccountTblNotifyEventNavigations")
                        .HasForeignKey("FkTblAccountTblNotifyEvent")
                        .IsRequired()
                        .HasConstraintName("FK_tblNotifyEvent_tblAccount");

                    b.HasOne("ScafoldDb.DAO.TblAccount", "FkTblAccountTblNotifyEventRecieverNavigation")
                        .WithMany("TblNotifyEventFkTblAccountTblNotifyEventRecieverNavigations")
                        .HasForeignKey("FkTblAccountTblNotifyEventReciever")
                        .IsRequired()
                        .HasConstraintName("FK_tblNotifyEvent_tblAccount1");

                    b.HasOne("ScafoldDb.DAO.TblNotifyEventType", "FkTblNotifyEventTypeTblNotifyEventNavigation")
                        .WithMany("TblNotifyEvents")
                        .HasForeignKey("FkTblNotifyEventTypeTblNotifyEvent")
                        .IsRequired()
                        .HasConstraintName("FK_tblNotifyEvent_tblNotifyEventType");

                    b.Navigation("FkTblAccountTblNotifyEventNavigation");

                    b.Navigation("FkTblAccountTblNotifyEventRecieverNavigation");

                    b.Navigation("FkTblNotifyEventTypeTblNotifyEventNavigation");
                });

            modelBuilder.Entity("ScafoldDb.DAO.TblAccount", b =>
                {
                    b.Navigation("TblNotifyEventFkTblAccountTblNotifyEventNavigations");

                    b.Navigation("TblNotifyEventFkTblAccountTblNotifyEventRecieverNavigations");
                });

            modelBuilder.Entity("ScafoldDb.DAO.TblNotifyEventType", b =>
                {
                    b.Navigation("TblNotifyEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
