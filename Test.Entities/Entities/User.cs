using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test.Entities.Entities
{
    public class User
    {
        [Key]
        public string LoginName { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int Salary { get; set; }
        public string ProfilePicture { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }        
    }
}
