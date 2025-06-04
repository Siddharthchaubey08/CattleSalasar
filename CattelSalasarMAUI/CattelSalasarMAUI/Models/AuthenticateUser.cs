using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Models
{
    public class AuthenticateUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ImeiNo { get; set; }
        public bool RememberMe { get; set; }
    }

   
}
