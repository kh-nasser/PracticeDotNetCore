using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepositoryPattern_ModelClass.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(150)]
        [Required]
        public string Family { get; set; }
        [MaxLength(150)]
        public string WebSite { get; set; }

    }
}