﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}