using System;
using System.Collections.Generic;

namespace WebWareHouse.Models
{
    public partial class Good
    {
        public Good()
        {
            InvoicesLists = new HashSet<InvoicesList>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Cost { get; set; }

        public virtual ICollection<InvoicesList> InvoicesLists { get; set; }
    }
}
