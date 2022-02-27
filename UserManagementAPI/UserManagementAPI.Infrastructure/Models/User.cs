using System;
using System.Collections.Generic;

#nullable disable

namespace UserManagementAPI.Infrastructure.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string ImageName { get; set; }
    }
}
