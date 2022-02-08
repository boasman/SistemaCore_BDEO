using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCore_BDEO.Models.Intervention
{
    public class LoginResponse
    {

        public bool success { get; set; }
        public string access_token { get; set; }
        public User entity { get; set; }
        public bool expired { get; set; }

    }
}