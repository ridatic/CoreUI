using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class DbFirstContext : DbContext
    {
        private Dictionary<Type, object> ActionsDictionary;

        public DbFirstContext()
        {
        }

        public DbFirstContext(DbContextOptions<DbFirstContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("Tenant");

                entity.Property(e => e.Tname)
                    .IsRequired()
                    .HasColumnName("TName")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TnbrSalaries).HasColumnName("TNbrSalaries");

                entity.Property(e => e.TnbrSocietes).HasColumnName("TNbrSocietes");

                entity.Property(e => e.Tnote)
                    .HasColumnName("TNote")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Towner)
                    .IsRequired()
                    .HasColumnName("TOwner")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Tstatus).HasColumnName("TStatus");

                entity.Property(e => e.Turl)
                    .IsRequired()
                    .HasColumnName("TUrl");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.ToTable("Utilisateur");

                entity.Property(e => e.UsrEmail)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UsrIdExtern)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UsrLogin)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UsrName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UsrPasswordHash)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UsrTel)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwAthorisation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAthorisation");

                entity.Property(e => e.RolAuthorisations).IsUnicode(false);

                entity.Property(e => e.RolName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ScDebutEx).HasColumnType("date");

                entity.Property(e => e.ScFinEx).HasColumnType("date");

                entity.Property(e => e.ScName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ScSourceType)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ScType)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UsrEmail)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UsrLogin)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UsrName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

        }

        public T GetAction<T>() where T : DBAction
        {
            if (ActionsDictionary == null) ActionsDictionary = new Dictionary<Type, object>();
            if (!ActionsDictionary.ContainsKey(typeof(T)))
            {
                ActionsDictionary.Add(typeof(T), (T)Activator.CreateInstance(typeof(T), this));
            }

            return (T)ActionsDictionary[typeof(T)];
        }

        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
        public virtual DbSet<VwAthorisation> VwAthorisations { get; set; }

        public AuthorisationQuery AuthorisationQueries => GetAction<AuthorisationQuery>();
    }

    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }
        }
    }
}