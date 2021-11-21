using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryConsoleCrudApp.Models.Entities
{
    class Author
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
