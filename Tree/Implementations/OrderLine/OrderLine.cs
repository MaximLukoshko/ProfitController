using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.BaseEnums;
using Tree.Interfaces;

namespace Tree.Implementations.OrderLine
{
    public class OrderLine : IOrderLine
    {
        public int Year { get; set; }
        public MonthEn Month { get; set; }
        public int Day { get; set; }
        public string DeviceName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string JobType { get; set; }
        public string InstalledDetails { get; set; }
        public double Income { get; set; }
        public double Outgo { get; set; }
        public bool CanHasSubOrders { get; set; }
    }
}
