﻿using System.ComponentModel.DataAnnotations;

namespace token_based_authentication.Data.ViewModels
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string EmailAddrress{ get; set; }
        [Required]
        public string UserName{ get; set; }
        [Required]
        public string Password{ get; set; }
        [Required]
        public string Role{ get; set; }
    }
}
