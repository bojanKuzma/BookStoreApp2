using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApp.Models;

[Table("BookRequests")]
public class BookRequest
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string? Author { get; set; }
    
    [Range(1000, 9999, ErrorMessage = "Please enter a valid 4-digit year")]
    public int PublicationYear { get; set; }
    
    public DateTime RequestDate { get; set; } = DateTime.Now;
}