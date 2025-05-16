using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApp.Models;

[Table("Books")]
public class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    public string? ShortDescription { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    public string? ImageUrl { get; set; }

    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<Author> Authors { get; set; } = new List<Author>();
}