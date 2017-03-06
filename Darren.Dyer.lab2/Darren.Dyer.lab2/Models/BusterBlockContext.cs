namespace Darren.Dyer.lab2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BusterBlockContext : DbContext
    {
        public BusterBlockContext()
            : base("name=BusterBlockContext")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<RentalOrder> RentalOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.RentalOrders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.RentalOrders)
                .WithMany(e => e.Items)
                .Map(m => m.ToTable("ItemRentalOrder").MapLeftKey("Items_ItemId").MapRightKey("RentalOrders_RentalOrderId"));
        }
    }
}
