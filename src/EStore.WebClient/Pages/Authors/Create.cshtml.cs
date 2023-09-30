using EBookStore.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace EStore.WebClient.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly string Url = "https://localhost:7089/odata/Authors";
        private readonly HttpClient httpClient;
        public CreateModel()
        {
            httpClient = new HttpClient();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AuthorDto Author { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await httpClient.PostAsJsonAsync($"{Url}", Author);

            if (!response.IsSuccessStatusCode)
            {
                if (response is { StatusCode: HttpStatusCode.NotFound })
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }

            }

            return RedirectToPage("./Index");
        }
    }
}
