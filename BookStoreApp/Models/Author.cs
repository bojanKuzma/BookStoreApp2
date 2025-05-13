using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApp.Models;


[Table("Authors")]
public class Author
{
    [Key]
    public int Id { get; set; }
    [MaxLength(70)]
    public required string FirstName { get; set; }
    [MaxLength(120)]
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}