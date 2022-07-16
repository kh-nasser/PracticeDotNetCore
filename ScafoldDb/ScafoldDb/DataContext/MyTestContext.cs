using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ScafoldDb.DAO;

namespace ScafoldDb.DataContext
{
    public partial class MyTestContext : DbContext
    {
        public MyTestContext()
        {
        }

        public MyTestContext(DbContextOptions<MyTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAccount> TblAccounts { get; set; } = null!;
        public virtual DbSet<TblNotifyEvent> TblNotifyEvents { get; set; } = null!;
        public virtual DbSet<TblNotifyEventType> TblNotifyEventTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=TestDb;User ID=sa;Password=1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAccount>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblNotifyEvent>(entity =>
            {
                entity.Property(e => e.BatchId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FkTblAccountTblNotifyEventNavigation)
                    .WithMany(p => p.TblNotifyEventFkTblAccountTblNotifyEventNavigations)
                    .HasForeignKey(d => d.FkTblAccountTblNotifyEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblNotifyEvent_tblAccount");

                entity.HasOne(d => d.FkTblAccountTblNotifyEventRecieverNavigation)
                    .WithMany(p => p.TblNotifyEventFkTblAccountTblNotifyEventRecieverNavigations)
                    .HasForeignKey(d => d.FkTblAccountTblNotifyEventReciever)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblNotifyEvent_tblAccount1");

                entity.HasOne(d => d.FkTblNotifyEventTypeTblNotifyEventNavigation)
                    .WithMany(p => p.TblNotifyEvents)
                    .HasForeignKey(d => d.FkTblNotifyEventTypeTblNotifyEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblNotifyEvent_tblNotifyEventType");
            });

            modelBuilder.Entity<TblNotifyEventType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Title).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
