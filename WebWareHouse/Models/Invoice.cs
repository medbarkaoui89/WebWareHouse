using System;
using System.Collections.Generic;

namespace WebWareHouse.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoicesLists = new HashSet<InvoicesList>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string TypeInvo { get; set; } = null!;
        public int IdShip { get; set; }

        public virtual Warehouse From1 { get; set; } = null!;
        public virtual Supplier FromNavigation { get; set; } = null!;
        public virtual Shipment IdShipNavigation { get; set; } = null!;
        public virtual Warehouse To1 { get; set; } = null!;
        public virtual Consumer ToNavigation { get; set; } = null!;
        public virtual ICollection<InvoicesList> InvoicesLists { get; set; }
    }
}
