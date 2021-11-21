using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryConsoleCrudApp.Models.Entities
{
    class Book
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Isbn { get; set; }
        [DefaultValue(1)]
        public int Edition { get; set; }
        [Timestamp]
        public byte[] PublishedAt { get; set; }

        public virtual ICollection<BookGenres> BooksGenres { get; set; }
    }
}
