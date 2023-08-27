using System;
using System.Collections.Generic;

namespace User_Management_Service.Context
{
    public partial class User
    {
        public User()
        {
            Tokens = new HashSet<Token>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDelete { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
