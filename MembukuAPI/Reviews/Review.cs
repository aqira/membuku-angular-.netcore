using System;
using System.Collections.Generic;
using MembukuAPI.Books;
using MembukuAPI.Users;

namespace MembukuAPI.Reviews
{
    public partial class Review
    {
        public string Username { get; set; } = null!;
        public int BookId { get; set; }
        public DateTime AddedDate { get; set; }
        public string ReadStatus { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? ReadDate { get; set; }
        public int? Rating { get; set; }
        public DateTime? ReviewDate { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual User UsernameNavigation { get; set; } = null!;
    }
}
