using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniversitySchedule.Core.Models.ViewModel
{
    public class RegistrationViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
    }
}
