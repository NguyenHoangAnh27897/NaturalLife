﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NaturalLife.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NaturalLifeEntities : DbContext
    {
        public NaturalLifeEntities()
            : base("name=NaturalLifeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<NTL_Booking> NTL_Booking { get; set; }
        public virtual DbSet<NTL_Booking_Activity> NTL_Booking_Activity { get; set; }
        public virtual DbSet<NTL_Booking_Service> NTL_Booking_Service { get; set; }
        public virtual DbSet<NTL_Customer> NTL_Customer { get; set; }
        public virtual DbSet<NTL_Inventory> NTL_Inventory { get; set; }
        public virtual DbSet<NTL_Room> NTL_Room { get; set; }
        public virtual DbSet<NTL_RoomType> NTL_RoomType { get; set; }
        public virtual DbSet<NTL_Schedule_Activity> NTL_Schedule_Activity { get; set; }
        public virtual DbSet<NTL_Schedule_Service> NTL_Schedule_Service { get; set; }
        public virtual DbSet<NTL_Service> NTL_Service { get; set; }
        public virtual DbSet<NTL_Vehicle> NTL_Vehicle { get; set; }
        public virtual DbSet<NTL_DiscountProgram> NTL_DiscountProgram { get; set; }
        public virtual DbSet<NTL_ExtraBooking> NTL_ExtraBooking { get; set; }
        public virtual DbSet<NTL_Adventure> NTL_Adventure { get; set; }
        public virtual DbSet<NTL_Main> NTL_Main { get; set; }
        public virtual DbSet<NTL_Blog> NTL_Blog { get; set; }
        public virtual DbSet<NTL_Contact> NTL_Contact { get; set; }
        public virtual DbSet<NTL_About> NTL_About { get; set; }
        public virtual DbSet<NTL_Setting> NTL_Setting { get; set; }
        public virtual DbSet<NTL_Slider> NTL_Slider { get; set; }
        public virtual DbSet<NTL_Schedule> NTL_Schedule { get; set; }
        public virtual DbSet<NTL_Video> NTL_Video { get; set; }
        public virtual DbSet<NTL_Icon> NTL_Icon { get; set; }
    }
}
