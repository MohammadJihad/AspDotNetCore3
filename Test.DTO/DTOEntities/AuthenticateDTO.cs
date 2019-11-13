using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DTO.DTOEntities
{
    public class AuthenticateDTO
    {
        public string LoginName { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
