using System;
using System.Collections.Generic;

#nullable disable

namespace eshop_webapi.Models
{
    public partial class SalePerson
    {
        public int SalePersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public virtual Order Order { get; set; }
    }
}
