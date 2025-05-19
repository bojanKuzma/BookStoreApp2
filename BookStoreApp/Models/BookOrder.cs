using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApp.Models;

public class BookOrder
{
    [Key]
    public int Id { get; set; }
        
    public DateTime OrderDate { get; set; } = DateTime.Now;
    
    // true = Approved, false = Rejected, null = Pending
    public bool? IsApproved { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    
    [ForeignKey("BookId")]
    public virtual Book Book { get; set; }
        
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}