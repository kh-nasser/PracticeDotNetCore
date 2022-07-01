using System;
using System.Collections.Generic;

#nullable disable

namespace eshop_webapi.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public double? Total { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public int SelesPersonId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual SalePerson OrderNavigation { get; set; }
        public virtual OrderItem OrderItem { get; set; }
    }
}
