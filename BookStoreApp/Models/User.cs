using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApp.Models;

[Table("Users")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    

    [MaxLength(20)]
    public string Role { get; set; } = "User";

    [MaxLength(20)]
    public string ThemeColor { get; set; } = "#673AB7";
    
    public bool DarkTheme { get; set; } = false;
}