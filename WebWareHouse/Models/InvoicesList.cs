using System;
using System.Collections.Generic;

namespace WebWareHouse.Models
{
    public partial class InvoicesList
    {
        public int Id { get; set; }
        public int IdGoods { get; set; }
        public int IdInvo { get; set; }
        public int Number { get; set; }

        public virtual Good IdGoodsNavigation { get; set; } = null!;
        public virtual Invoice IdInvoNavigation { get; set; } = null!;
    }
}
