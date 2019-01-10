using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM_Machine.Models
{
    public class UserDetails
    {
        public int AccountId { get; set; }
        public long CardNo { get; set; }
        public int CardPin { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstame { get; set; }
        public string LastName { get; set; }
    }
}