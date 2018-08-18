using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAspNetCoreLogin.Models
{
    public class UserLoginViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }    
    }
}
