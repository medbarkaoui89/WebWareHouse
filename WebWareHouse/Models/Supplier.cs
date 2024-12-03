using System;
using System.Collections.Generic;

namespace WebWareHouse.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte[]? Photo { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
