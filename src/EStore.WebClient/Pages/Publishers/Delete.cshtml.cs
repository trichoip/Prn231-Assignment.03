using EBookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace EStore.WebClient.Pages.Publishers
{
    public class DeleteModel : PageModel
    {
        private readonly string Url = "https://localhost:7089/odata/Publishers";
        private readonly HttpClient httpClient;
        public DeleteModel()
        {
            httpClient = new HttpClient();
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await httpClient.GetAsync($"{Url}/{id}");

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
            Publisher = await response.Content.ReadFromJsonAsync<Publisher>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await httpClient.DeleteAsync($"{Url}/{Publisher.PubId}");

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
