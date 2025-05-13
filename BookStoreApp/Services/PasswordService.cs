using System.Security.Cryptography;
using System.Text;

namespace BookStoreApp.Services;

public abstract class PasswordService
{
    public static (string Hash, string Salt) HashPassword(string password)
    {
        var salt = Guid.NewGuid().ToString("N");
        using var sha256 = SHA256.Create();
        var hash = Convert.ToBase64String(
            sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt)));
        return (hash, salt);
    }

    public static bool VerifyPassword(string password, string salt, string hash)
    {
        using var sha256 = SHA256.Create();
        var computedHash = Convert.ToBase64String(
            sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt)));
        return computedHash == hash;
    }
}