using System;
using System.Collections.Generic;

#nullable disable

namespace eshop_webapi.Models
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Order OrderItemNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
