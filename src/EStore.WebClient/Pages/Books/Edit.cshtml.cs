using EBookStore.Application.DTOs;
using EBookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Net;

namespace EStore.WebClient.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly string Url = "https://localhost:7089/odata/Books";
        private readonly HttpClient httpClient;
        public EditModel()
        {
            httpClient = new HttpClient();
        }

        [BindProperty]
        public BookDto Book { get; set; } = default!;

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
            Book = await response.Content.ReadFromJsonAsync<BookDto>();
            response = await httpClient.GetAsync("https://localhost:7089/odata/Publishers");
            var data = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(data);
            var Publisher = temp.value?.ToObject<IList<Publisher>>() ?? Array.Empty<Publisher>();
            ViewData["PubId"] = new SelectList(Publisher, "PubId", "PublisherName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await httpClient.PutAsJsonAsync($"{Url}/{Book.BookId}", Book);

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
