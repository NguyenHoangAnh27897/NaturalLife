using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NaturalLife.Models;

namespace NaturalLife.Models
{
    public class Data
    {
        public class HomePage
        {
            public List<NTL_RoomType> RoomType { get; set; }
            public IQueryable<NTL_Slider> Slider { get; set; }
            public IQueryable<NTL_Main> Main { get; set; }
            public List<NTL_Service> Service { get; set; }
            public List<NTL_DiscountProgram> Discount { get; set; }

        }

        public class ServiceDetail
        {
            public List<NTL_Adventure> Adventure { get; set; }
            public List<NTL_Schedule> Schedule { get; set; }
            public IQueryable<NTL_Service> Service { get; set; }
        }

        public class RoomDetail
        {
            public IQueryable<NTL_Schedule> Schedule { get; set; }
            public IQueryable<NTL_Room> Room { get; set; }
        }

        public class Discount
        {
            public List<NTL_Room> room { get; set; }
            public List<NTL_Service> service { get; set; }
        }
    }
}