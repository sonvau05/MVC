using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models
{
    public class Borrow
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required, StringLength(100)]
        public string BorrowerName { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }
    }
}