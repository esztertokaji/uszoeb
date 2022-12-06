using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace UszoEb.Models
{
    public partial class uszoebContext : DbContext
    {
        public uszoebContext()
        {
        }

        public uszoebContext(DbContextOptions<uszoebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Orszagok> Orszagoks { get; set; }
        public virtual DbSet<Szamok> Szamoks { get; set; }
        public virtual DbSet<Versenyzok> Versenyzoks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;database=uszoeb;user=root;password=;ssl mode=none;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orszagok>(entity =>
            {
                entity.ToTable("orszagok");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("nev");

                entity.Property(e => e.Nobid)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("nobid");
            });

            modelBuilder.Entity<Szamok>(entity =>
            {
                entity.ToTable("szamok");

                entity.HasIndex(e => e.VersenyzoId, "versenyzoId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("nev");

                entity.Property(e => e.VersenyzoId)
                    .HasColumnType("int(11)")
                    .HasColumnName("versenyzoId");

                entity.HasOne(d => d.Versenyzo)
                    .WithMany(p => p.Szamoks)
                    .HasForeignKey(d => d.VersenyzoId)
                    .HasConstraintName("szamok_ibfk_1");
            });

            modelBuilder.Entity<Versenyzok>(entity =>
            {
                entity.ToTable("versenyzok");

                entity.HasIndex(e => e.OrszagId, "orszagId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Nem)
                    .IsRequired()
                    .HasColumnName("nem");

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("nev");

                entity.Property(e => e.OrszagId)
                    .HasColumnType("int(11)")
                    .HasColumnName("orszagId");

                entity.HasOne(d => d.Orszag)
                    .WithMany(p => p.Versenyzoks)
                    .HasForeignKey(d => d.OrszagId)
                    .HasConstraintName("versenyzok_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
