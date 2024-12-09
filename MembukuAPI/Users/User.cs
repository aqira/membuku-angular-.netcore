using System;
using System.Collections.Generic;
using MembukuAPI.Reviews;

namespace MembukuAPI.Users
{
    public partial class User
    {
        public User()
        {
            Reviews = new HashSet<Review>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? Gender { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
