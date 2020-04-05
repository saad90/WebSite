﻿using System;
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
        public virtual DbSet<IxAntenne> IxAntenne { get; set; }
        public virtual DbSet<IxMaterielsStatuts> IxMaterielsStatuts { get; set; }
        public virtual DbSet<IxMaterielsTypes> IxMaterielsTypes { get; set; }
        public virtual DbSet<LinksMaterielsIxMaterielStatuts> LinksMaterielsIxMaterielStatuts { get; set; }
        public virtual DbSet<Materiels> Materiels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
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
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

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

            modelBuilder.Entity<IxAntenne>(entity =>
            {
                entity.HasKey(e => e.PkAntenne)
                    .HasName("PK__Ix_Anten__B9C5C310302600E7");

                entity.ToTable("Ix_Antenne");

                entity.Property(e => e.PkAntenne).HasColumnName("Pk_Antenne");

                entity.Property(e => e.CodePostal)
                    .HasColumnName("Code_postal")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ville)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IxMaterielsStatuts>(entity =>
            {
                entity.HasKey(e => e.PkIxMaterielsStatuts)
                    .HasName("PK__Ix_Mater__4C088D1AD2805C63");

                entity.ToTable("Ix_Materiels_Statuts");

                entity.Property(e => e.PkIxMaterielsStatuts).HasColumnName("Pk_Ix_Materiels_Statuts");

                entity.Property(e => e.MaterielStatut)
                    .HasColumnName("Materiel_Statut")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IxMaterielsTypes>(entity =>
            {
                entity.HasKey(e => e.PkIxMaterielsTypes)
                    .HasName("PK__Ix_Mater__8CF992DE88FB1410");

                entity.ToTable("Ix_Materiels_Types");

                entity.Property(e => e.PkIxMaterielsTypes).HasColumnName("Pk_Ix_Materiels_Types");

                entity.Property(e => e.MaterielType)
                    .HasColumnName("Materiel_Type")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LinksMaterielsIxMaterielStatuts>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("PK__LINKS_MA__321507E7679EF663");

                entity.ToTable("LINKS_MATERIELS_IX_MATERIEL_STATUTS");

                entity.Property(e => e.Commentaire).HasMaxLength(255);

                entity.Property(e => e.DateDebut)
                    .HasColumnName("Date_Debut")
                    .HasColumnType("date");

                entity.Property(e => e.DateFin)
                    .HasColumnName("Date_fin")
                    .HasColumnType("date");

                entity.Property(e => e.FkAspNetUsers)
                    .HasColumnName("Fk_AspNetUsers")
                    .HasMaxLength(450);

                entity.Property(e => e.FkIxAntenne).HasColumnName("Fk_Ix_Antenne");

                entity.Property(e => e.FkMateriels).HasColumnName("Fk_Materiels");

                entity.Property(e => e.FkMaterielsStatuts).HasColumnName("Fk_Materiels_Statuts");

                entity.HasOne(d => d.FkAspNetUsersNavigation)
                    .WithMany(p => p.LinksMaterielsIxMaterielStatuts)
                    .HasForeignKey(d => d.FkAspNetUsers)
                    .HasConstraintName("FK__LINKS_MAT__Fk_As__4222D4EF");

                entity.HasOne(d => d.FkIxAntenneNavigation)
                    .WithMany(p => p.LinksMaterielsIxMaterielStatuts)
                    .HasForeignKey(d => d.FkIxAntenne)
                    .HasConstraintName("FK__LINKS_MAT__Fk_Ix__4316F928");

                entity.HasOne(d => d.FkMaterielsNavigation)
                    .WithMany(p => p.LinksMaterielsIxMaterielStatuts)
                    .HasForeignKey(d => d.FkMateriels)
                    .HasConstraintName("FK__LINKS_MAT__Fk_Ma__440B1D61");

                entity.HasOne(d => d.FkMaterielsStatutsNavigation)
                    .WithMany(p => p.LinksMaterielsIxMaterielStatuts)
                    .HasForeignKey(d => d.FkMaterielsStatuts)
                    .HasConstraintName("FK__LINKS_MAT__Fk_Ma__44FF419A");
            });

            modelBuilder.Entity<Materiels>(entity =>
            {
                entity.HasKey(e => e.PkMateriels)
                    .HasName("PK__Materiel__7FF74FBBA9B0F3AF");

                entity.Property(e => e.PkMateriels).HasColumnName("Pk_Materiels");

                entity.Property(e => e.DateAchat)
                    .HasColumnName("Date_Achat")
                    .HasColumnType("date");

                entity.Property(e => e.FkMaterielsTypes).HasColumnName("Fk_Materiels_TYPES");

                entity.Property(e => e.Identifiant)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkMaterielsTypesNavigation)
                    .WithMany(p => p.Materiels)
                    .HasForeignKey(d => d.FkMaterielsTypes)
                    .HasConstraintName("FK__Materiels__Fk_Ma__3F466844");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}