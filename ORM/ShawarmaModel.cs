namespace ORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShawarmaModel : DbContext
    {
        public ShawarmaModel()
            : base("name=ShawarmaModel")
        {
        }

        public virtual DbSet<Ingradient> Ingradient { get; set; }
        public virtual DbSet<IngradientCategory> IngradientCategory { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<OrderHeader> OrderHeader { get; set; }
        public virtual DbSet<PriceController> PriceController { get; set; }
        public virtual DbSet<Seller> Seller { get; set; }
        public virtual DbSet<SellingPoint> SellingPoint { get; set; }
        public virtual DbSet<SellingPointCategory> SellingPointCategory { get; set; }
        public virtual DbSet<Shawarma> Shawarma { get; set; }
        public virtual DbSet<ShawarmaRecipe> ShawarmaRecipe { get; set; }
        public virtual DbSet<TimeController> TimeController { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingradient>()
                .Property(e => e.IngradientName)
                .IsUnicode(false);

            modelBuilder.Entity<Ingradient>()
                .HasMany(e => e.ShawarmaRecipe)
                .WithRequired(e => e.Ingradient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IngradientCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<IngradientCategory>()
                .HasMany(e => e.Ingradient)
                .WithRequired(e => e.IngradientCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderHeader>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.OrderHeader)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceController>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PriceController>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Seller>()
                .Property(e => e.SellerName)
                .IsUnicode(false);

            modelBuilder.Entity<Seller>()
                .HasMany(e => e.OrderHeader)
                .WithRequired(e => e.Seller)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seller>()
                .HasMany(e => e.TimeController)
                .WithRequired(e => e.Seller)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SellingPoint>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<SellingPoint>()
                .Property(e => e.ShawarmaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<SellingPoint>()
                .HasMany(e => e.PriceController)
                .WithRequired(e => e.SellingPoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SellingPoint>()
                .HasMany(e => e.Seller)
                .WithRequired(e => e.SellingPoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SellingPointCategory>()
                .Property(e => e.SellingPointCategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<SellingPointCategory>()
                .HasMany(e => e.SellingPoint)
                .WithRequired(e => e.SellingPointCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shawarma>()
                .Property(e => e.ShawarmaName)
                .IsUnicode(false);

            modelBuilder.Entity<Shawarma>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Shawarma)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shawarma>()
                .HasMany(e => e.PriceController)
                .WithRequired(e => e.Shawarma)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shawarma>()
                .HasMany(e => e.ShawarmaRecipe)
                .WithRequired(e => e.Shawarma)
                .WillCascadeOnDelete(false);
        }
    }
}
