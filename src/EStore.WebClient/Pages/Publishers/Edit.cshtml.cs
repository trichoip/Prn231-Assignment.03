using EBookStore.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace EStore.WebClient.Pages.Publishers
{
    public class EditModel : PageModel
    {
        private readonly string Url = "https://localhost:7089/odata/Publishers";
        private readonly HttpClient httpClient;
        public EditModel()
        {
            httpClient = new HttpClient();
        }

        [BindProperty]
        public PublisherDto Publisher { get; set; } = default!;

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
            Publisher = await response.Content.ReadFromJsonAsync<PublisherDto>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await httpClient.PutAsJsonAsync($"{Url}/{Publisher.PubId}", Publisher);

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
