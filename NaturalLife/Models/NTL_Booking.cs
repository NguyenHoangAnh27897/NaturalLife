//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class NTL_Booking
    {
        public string ID { get; set; }
        public string CustomerID { get; set; }
        public string NumberOfAdult { get; set; }
        public string NumberOfChild { get; set; }
        public Nullable<int> VehicleID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> DiscountID { get; set; }
        public Nullable<int> RoomType { get; set; }
    }
}
