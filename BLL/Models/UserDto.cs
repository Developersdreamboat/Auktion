using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class UserDto
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }
}
