using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Author { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}