using EBookStore.Application.DTOs;
using EBookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Net;

namespace EStore.WebClient.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly string Url = "https://localhost:7089/odata/Books";
        private readonly HttpClient httpClient;
        public CreateModel()
        {
            httpClient = new HttpClient();
        }

        public async Task<IActionResult> OnGet()
        {
            var response = await httpClient.GetAsync("https://localhost:7089/odata/Publishers");
            var data = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(data);
            var Publisher = temp.value?.ToObject<IList<Publisher>>() ?? Array.Empty<Publisher>();
            ViewData["PubId"] = new SelectList(Publisher, "PubId", "PublisherName");
            return Page();
        }

        [BindProperty]
        public BookDto Book { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await httpClient.PostAsJsonAsync($"{Url}", Book);

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
