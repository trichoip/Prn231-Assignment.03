using EBookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace EStore.WebClient.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        private readonly string Url = "https://localhost:7089/odata/Authors";
        private readonly HttpClient httpClient;
        public DetailsModel()
        {
            httpClient = new HttpClient();
        }

        public Author Author { get; set; } = default!;

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
            Author = await response.Content.ReadFromJsonAsync<Author>();
            return Page();
        }
    }
}
