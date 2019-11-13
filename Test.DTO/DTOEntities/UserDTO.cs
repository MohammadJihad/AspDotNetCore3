using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DTO.DTOEntities
{
    public class UserDTO
    {
        public string LoginName { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int Salary { get; set; }
        public string ProfilePicture { get; set; }
        public string Password { get; set; }
        public int CountryId { get; set; }
        public virtual CountryDTO Country { get; set; }
        public string CountryName { get => Country?.Name; }
    }
}
