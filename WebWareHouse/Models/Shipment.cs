using System;
using System.Collections.Generic;

namespace WebWareHouse.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int IdCom { get; set; }
        public double Tariff { get; set; }
        public TimeSpan Time { get; set; }
        public double Cost { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
