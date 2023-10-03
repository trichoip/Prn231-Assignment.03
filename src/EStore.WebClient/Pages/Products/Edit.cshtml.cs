using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EStore.WebClient.Pages.Products
{
    public class EditModel : PageModel
    {

        public IActionResult OnGet(int? id)
        {

            return Page();
        }

    }
}
