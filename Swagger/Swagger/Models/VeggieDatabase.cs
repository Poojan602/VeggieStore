namespace Swagger.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VeggieDatabase : DbContext
    {
        public VeggieDatabase()
            : base("name=VeggieDatabase")
        {
        }

        public virtual DbSet<area> areas { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<dealer> dealers { get; set; }
        public virtual DbSet<login> logins { get; set; }
        public virtual DbSet<subscription> subscriptions { get; set; }
        public virtual DbSet<dealer_area> dealer_areas { get; set; }
        public virtual DbSet<dealer_slot> dealer_slots { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<area>()
                .Property(e => e.Area_Name)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.C_Name)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.C_Email)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.C_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.C_Address)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.C_Area)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.C_Pass)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.D_Name)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.D_Address)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.D_Area)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.D_Zipcode)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.D_Phno)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.D_Email)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.D_Pass)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.D_Subscription)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .HasMany(e => e.dealer_area)
                .WithRequired(e => e.dealer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<dealer>()
                .HasMany(e => e.dealer_slots)
                .WithRequired(e => e.dealer)
                .HasForeignKey(e => e.dealer_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<login>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<login>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<subscription>()
                .Property(e => e.S_Name)
                .IsUnicode(false);

            modelBuilder.Entity<dealer_area>()
                .Property(e => e.Area)
                .IsUnicode(false);

            modelBuilder.Entity<dealer_slot>()
                .Property(e => e.Before_After)
                .IsUnicode(false);

            modelBuilder.Entity<dealer_slot>()
                .Property(e => e.TimeSlot)
                .IsUnicode(false);
        }
    }
}
