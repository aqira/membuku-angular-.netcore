using System;
using System.Collections.Generic;
using MembukuAPI.Books;

namespace MembukuAPI.HighlightedBooks
{
    public partial class HighlightedBook
    {
        public int BookId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime AddedDate { get; set; }

        public virtual Book Book { get; set; } = null!;
    }
}
