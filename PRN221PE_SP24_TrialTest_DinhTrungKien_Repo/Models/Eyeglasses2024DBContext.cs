using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien_Repo.Models
{
    public partial class Eyeglasses2024DBContext : DbContext
    {
        public Eyeglasses2024DBContext()
        {
        }

        public Eyeglasses2024DBContext(DbContextOptions<Eyeglasses2024DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Eyeglass> Eyeglasses { get; set; } = null!;
        public virtual DbSet<LensType> LensTypes { get; set; } = null!;
        public virtual DbSet<StoreAccount> StoreAccounts { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=localhost;Database=Eyeglasses2024DB;Uid=sa;Pwd=12345;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Eyeglass>(entity =>
            {
                entity.HasKey(e => e.EyeglassesId)
                    .HasName("PK__Eyeglass__93A83C7BBFFE7EDE");

                entity.Property(e => e.EyeglassesId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EyeglassesDescription).HasMaxLength(250);

                entity.Property(e => e.EyeglassesName).HasMaxLength(100);

                entity.Property(e => e.FrameColor).HasMaxLength(50);

                entity.Property(e => e.LensTypeId).HasMaxLength(30);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.LensType)
                    .WithMany(p => p.Eyeglasses)
                    .HasForeignKey(d => d.LensTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Eyeglasse__LensT__3C69FB99");
            });

            modelBuilder.Entity<LensType>(entity =>
            {
                entity.ToTable("LensType");

                entity.Property(e => e.LensTypeId).HasMaxLength(30);

                entity.Property(e => e.LensTypeDescription).HasMaxLength(250);

                entity.Property(e => e.LensTypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<StoreAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__StoreAcc__349DA586BFA92B9C");

                entity.ToTable("StoreAccount");

                entity.HasIndex(e => e.EmailAddress, "UQ__StoreAcc__49A14740312B0E6F")
                    .IsUnique();

                entity.Property(e => e.AccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("AccountID");

                entity.Property(e => e.AccountPassword).HasMaxLength(40);

                entity.Property(e => e.EmailAddress).HasMaxLength(60);

                entity.Property(e => e.FullName).HasMaxLength(60);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
