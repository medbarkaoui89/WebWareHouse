using System;
using System.Collections.Generic;

namespace WebWareHouse.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            InvoiceFrom1s = new HashSet<Invoice>();
            InvoiceTo1s = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Invoice> InvoiceFrom1s { get; set; }
        public virtual ICollection<Invoice> InvoiceTo1s { get; set; }
    }
}
