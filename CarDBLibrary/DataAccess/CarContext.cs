using CarDBLibrary.CommonTypes;
using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.DataAccess
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }
       
        public CarContext(DbContextOptionsBuilder optionsBuilder) : this(optionsBuilder.Options)
        {
        }
        public CarContext(string connStr) : this(new DbContextOptionsBuilder().UseSqlServer(connStr))
        {

        }
        private void CommonDefaultValues(params EntityTypeBuilder[] entityTypes)
        {
            foreach (var entityType in entityTypes)
            {
                entityType.Property("Id").HasDefaultValueSql("NEWID()");
                entityType.Property("CreatedOn").HasDefaultValueSql("GETDATE()");
                entityType.Property("Status").HasDefaultValue(Status.Active);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CommonDefaultValues(
                 modelBuilder.Entity<DescriptionCatalog>(),
                 modelBuilder.Entity<EquipmentCatalog>(),
                 modelBuilder.Entity<EquipmentVersionCatalog>(),
                 modelBuilder.Entity<FeatureNameCatalog>(),
                 modelBuilder.Entity<FeatureCatalog>(),
                 modelBuilder.Entity<InclusionCatalog>(),
                 modelBuilder.Entity<MakeCatalog>(),
                 modelBuilder.Entity<ModelCatalog>(),
                 modelBuilder.Entity<SpecificationCatalog>(),
                 modelBuilder.Entity<SpecificationInclusionCatalog>(),
                 modelBuilder.Entity<VersionCatalog>(),
                 modelBuilder.Entity<EquipmentTask>(),
                 modelBuilder.Entity<ColorCatalog>(),
                 modelBuilder.Entity<PricelistCatalog>(),
                 modelBuilder.Entity<ModificationCatalog>()
                );
        }
        public DbSet<DescriptionCatalog> DescriptionCatalog { get; set; }
        public DbSet<EquipmentCatalog> EquipmentCatalog { get; set; }
        public DbSet<EquipmentVersionCatalog> EquipmentVersionCatalog { get; set; }
        public DbSet<FeatureNameCatalog> FeatureNameCatalog { get; set; }
        public DbSet<FeatureCatalog> FeatureCatalog { get; set; }
        public DbSet<InclusionCatalog> InclusionCatalog { get; set; }
        public DbSet<MakeCatalog> MakeCatalog { get; set; }
        public DbSet<ModelCatalog> ModelCatalog { get; set; }
        public DbSet<SpecificationCatalog> SpecificationCatalog { get; set; }
        public DbSet<SpecificationInclusionCatalog> SpecificationInclusionCatalog { get; set; }
        public DbSet<VersionCatalog> VersionCatalog { get; set; }
        public DbSet<EquipmentTask> EquipmentTask { get; set; }
        public DbSet<ColorCatalog> ColorCatalog { get; set; }
        public DbSet<PricelistCatalog> PricelistCatalog { get; set; }
        public DbSet<ModificationCatalog> ModificationCatalog { get; set; }

    }
}
