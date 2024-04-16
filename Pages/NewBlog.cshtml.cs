using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace MyWebApp.Pages
{
    public class NewBlogModel : PageModel
    {
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Content { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validate input
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Content) || Image == null)
            {
                ModelState.AddModelError(string.Empty, "Please fill in all the required fields.");
                return Page();
            }

            var imagePath = Path.Combine("images", Image.FileName);
            var absoluteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);

            // Ensuring the images directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(absoluteImagePath));

            using (var fileStream = new FileStream(absoluteImagePath, FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }

            // Ensuring thread-safety and integrity of the BlogPosts.txt
            lock (this)
            {
                var blogPostDetails = $"{Title}||{Content}||{imagePath}\n";
                System.IO.File.AppendAllText("BlogPosts.txt", blogPostDetails);
            }

            return RedirectToPage("/Blog"); // Redirect to Blog page to see the post
        }
    }
}
