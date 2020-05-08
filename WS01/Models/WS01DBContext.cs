using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WS01.Models
{
    public partial class WS01DBContext : DbContext
    {
        public WS01DBContext()
        {
        }

        public WS01DBContext(DbContextOptions<WS01DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<IxAntenne> IxAntenne { get; set; }
        public virtual DbSet<IxInterventionsTypes> IxInterventionsTypes { get; set; }
        public virtual DbSet<IxMaterielsStatuts> IxMaterielsStatuts { get; set; }
        public virtual DbSet<IxMaterielsTypes> IxMaterielsTypes { get; set; }
        public virtual DbSet<LinksInterventions> LinksInterventions { get; set; }
        public virtual DbSet<LinksMaterielsIxMaterielStatuts> LinksMaterielsIxMaterielStatuts { get; set; }
        public virtual DbSet<Materiels> Materiels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SAAD-PC\\SQLEXPRESS;Database=WS01DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => e.Pk);

                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.PkIxContact);

                entity.Property(e => e.PkIxContact).HasColumnName("Pk_Ix_Contact");

                entity.Property(e => e.AdresseMail)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Objet)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            modelBuilder.Entity<IxAntenne>(entity =>
            {
                entity.HasKey(e => e.PkAntenne);

                entity.ToTable("Ix_Antenne");

                entity.Property(e => e.PkAntenne).HasColumnName("Pk_Antenne");

                entity.Property(e => e.Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CodePostal)
                    .IsRequired()
                    .HasColumnName("Code_Postal")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ville)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IxInterventionsTypes>(entity =>
            {
                entity.HasKey(e => e.PkIxInterventionsTypes);

                entity.ToTable("Ix_Interventions_Types");

                entity.Property(e => e.PkIxInterventionsTypes).HasColumnName("Pk_Ix_Interventions_Types");

                entity.Property(e => e.InterventionType)
                    .IsRequired()
                    .HasColumnName("Intervention_Type")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IxMaterielsStatuts>(entity =>
            {
                entity.HasKey(e => e.PkIxMaterielsStatuts);

                entity.ToTable("Ix_Materiels_Statuts");

                entity.Property(e => e.PkIxMaterielsStatuts).HasColumnName("Pk_Ix_Materiels_Statuts");

                entity.Property(e => e.MaterielStatut)
                    .HasColumnName("Materiel_Statut")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IxMaterielsTypes>(entity =>
            {
                entity.HasKey(e => e.PkIxMaterielsTypes);

                entity.ToTable("Ix_Materiels_Types");

                entity.Property(e => e.PkIxMaterielsTypes).HasColumnName("Pk_Ix_Materiels_Types");

                entity.Property(e => e.MaterielType)
                    .HasColumnName("Materiel_Type")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LinksInterventions>(entity =>
            {
                entity.HasKey(e => e.PkInterventions)
                    .HasName("PK_Ix_Interventions");

                entity.ToTable("LINKS_Interventions");

                entity.Property(e => e.PkInterventions).HasColumnName("Pk_Interventions");

                entity.Property(e => e.Commentaire).HasMaxLength(510);

                entity.Property(e => e.DateDebut)
                    .HasColumnName("Date_Debut")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.DateFin)
                    .HasColumnName("Date_Fin")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.FkAspNetUsers)
                    .IsRequired()
                    .HasColumnName("Fk_AspNetUsers")
                    .HasMaxLength(450);

                entity.Property(e => e.FkInterventionsTypes).HasColumnName("Fk_Interventions_Types");

                entity.Property(e => e.FkIxAntenne).HasColumnName("Fk_Ix_Antenne");

                entity.Property(e => e.FkMaterielsStatuts).HasColumnName("Fk_Materiels_Statuts");

                entity.HasOne(d => d.FkAspNetUsersNavigation)
                    .WithMany(p => p.LinksInterventions)
                    .HasForeignKey(d => d.FkAspNetUsers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LINKS_Int__Fk_As__68487DD7");

                entity.HasOne(d => d.FkInterventionsTypesNavigation)
                    .WithMany(p => p.LinksInterventions)
                    .HasForeignKey(d => d.FkInterventionsTypes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ix_Interv__Fk_In__4CA06362");

                entity.HasOne(d => d.FkIxAntenneNavigation)
                    .WithMany(p => p.LinksInterventions)
                    .HasForeignKey(d => d.FkIxAntenne)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LINKS_Int__Fk_Ix__6A30C649");

                entity.HasOne(d => d.FkMaterielsStatutsNavigation)
                    .WithMany(p => p.LinksInterventions)
                    .HasForeignKey(d => d.FkMaterielsStatuts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LINKS_Int__Fk_Ma__6B24EA82");
            });

            modelBuilder.Entity<LinksMaterielsIxMaterielStatuts>(entity =>
            {
                entity.HasKey(e => e.Pk);

                entity.ToTable("LINKS_MATERIELS_IX_MATERIEL_STATUTS");

                entity.Property(e => e.Commentaire).HasMaxLength(510);

                entity.Property(e => e.DateDebut)
                    .HasColumnName("Date_Debut")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.DateFin)
                    .HasColumnName("Date_Fin")
                    .HasMaxLength(60);

                entity.Property(e => e.FkAspNetUsers)
                    .IsRequired()
                    .HasColumnName("Fk_AspNetUsers")
                    .HasMaxLength(450);

                entity.Property(e => e.FkIxAntenne).HasColumnName("Fk_Ix_Antenne");

                entity.Property(e => e.FkMateriels).HasColumnName("Fk_Materiels");

                entity.Property(e => e.FkMaterielsStatuts).HasColumnName("Fk_Materiels_Statuts");

                entity.HasOne(d => d.FkAspNetUsersNavigation)
                    .WithMany(p => p.LinksMaterielsIxMaterielStatuts)
                    .HasForeignKey(d => d.FkAspNetUsers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LINKS_MAT__Fk_As__4222D4EF");

                entity.HasOne(d => d.FkIxAntenneNavigation)
                    .WithMany(p => p.LinksMaterielsIxMaterielStatuts)
                    .HasForeignKey(d => d.FkIxAntenne)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LINKS_MAT__Fk_Ix__3B75D760");

                entity.HasOne(d => d.FkMaterielsNavigation)
                    .WithMany(p => p.LinksMaterielsIxMaterielStatuts)
                    .HasForeignKey(d => d.FkMateriels)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LINKS_MAT__Fk_Ma__398D8EEE");

                entity.HasOne(d => d.FkMaterielsStatutsNavigation)
                    .WithMany(p => p.LinksMaterielsIxMaterielStatuts)
                    .HasForeignKey(d => d.FkMaterielsStatuts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LINKS_MAT__Fk_Ma__3A81B327");
            });

            modelBuilder.Entity<Materiels>(entity =>
            {
                entity.HasKey(e => e.PkMateriels);

                entity.Property(e => e.PkMateriels).HasColumnName("Pk_Materiels");

                entity.Property(e => e.FkMaterielsTypes).HasColumnName("Fk_Materiels_Types");

                entity.Property(e => e.Identifiant)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkMaterielsTypesNavigation)
                    .WithMany(p => p.Materiels)
                    .HasForeignKey(d => d.FkMaterielsTypes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Materiels__Fk_Ma__36B12243");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
