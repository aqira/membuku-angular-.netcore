﻿using System;
using System.Collections.Generic;
using MembukuAPI.Books;

namespace MembukuAPI.Authors
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
