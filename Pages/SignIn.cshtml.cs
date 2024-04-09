using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

      return RedirectToPage("/NewBlog"); // Redirect to NewBlog page
    }

  }
}
