using System;
using System.Collections.Generic;
using MembukuAPI.Authors;
using MembukuAPI.HighlightedBooks;
using MembukuAPI.Reviews;

namespace MembukuAPI.Books {
    public partial class Book {
        public Book() {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Cover { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? AuthorId { get; set; }

        public virtual Author? Author { get; set; }
        public virtual HighlightedBook HighlightedBook { get; set; } = null!;
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
