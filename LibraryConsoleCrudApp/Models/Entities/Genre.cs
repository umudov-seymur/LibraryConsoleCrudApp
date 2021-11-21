using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryConsoleCrudApp.Models.Entities
{
    class Genre
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<BookGenres> BooksGenres { get; set; }
    }
}
