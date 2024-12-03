using System;
using System.Collections.Generic;

namespace WebWareHouse.Models
{
    public partial class Consumer
    {
        public Consumer()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
