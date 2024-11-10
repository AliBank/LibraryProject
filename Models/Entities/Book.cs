using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace LibraryAppWeb.Models.Entities
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [Required]
        public int PublishedYear { get; set; }

        [Required]
        [MaxLength(100)]
        public string Genre { get; set; }

    }
}
