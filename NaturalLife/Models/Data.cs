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

        }
    }
}