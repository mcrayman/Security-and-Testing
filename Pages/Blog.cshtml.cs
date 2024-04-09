using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyWebApp.Pages
{
    public class BlogModel : PageModel
    {
        public List<BlogPost> Posts { get; set; } = new List<BlogPost>();

        public void OnGet()
        {
            var postsPath = "BlogPosts.txt";
            if (System.IO.File.Exists(postsPath))
            {
                var lines = System.IO.File.ReadAllLines(postsPath);
                Posts = lines.Select(line =>
                {
                    var parts = line.Split(new[] { "||" }, System.StringSplitOptions.None);
                    if (parts.Length < 3) // Ensure there are at least 3 parts for Title, Content, and ImagePath
                    {
                        return null; // Or handle invalid line format as needed
                    }
                    return new BlogPost
                    {
                        Title = parts[0],
                        Content = parts[1],
                        ImagePath = parts[2]
                    };
                })
                .Where(post => post != null) // Exclude any null entries resulting from invalid lines
                .ToList();
            }
        }
    }

    public class BlogPost
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
    }
}
