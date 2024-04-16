using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace MyWebApp.Pages
{
  public class SignInModel : PageModel
  {
    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      // Assuming user sign-in logic here
      string password = Request.Form["password"]; // Get the password from the request

      // Hash the password using SHA-256 with a salt
      string hashedPassword = HashPassword(password);

      // Implement rate limiting or account lockout logic here

      return RedirectToPage("/Home");

    }

    private string HashPassword(string password)
    {
      using (SHA256 sha256 = SHA256.Create())
      {
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
      }
    }

  }
}
