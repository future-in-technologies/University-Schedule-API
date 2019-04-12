using System;
using System.Collections.Generic;
using System.Text;
using UniversitySchedule.Core.Abstractions;
using UniversitySchedule.Core.Enums;

namespace UniversitySchedule.Core.Entites
{
    public class User : EntityBaseWithId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public UserRoles Role { get; set; }
    }
}
