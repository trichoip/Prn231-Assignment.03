using EBookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace EStore.WebClient.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly string Url = "https://localhost:7089/odata/Books?$expand=Publisher";
        private readonly HttpClient httpClient;
        public IndexModel()
        {
            httpClient = new HttpClient();
        }

        public IList<Book> Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await httpClient.GetAsync(Url);
            if (!response.IsSuccessStatusCode)
            {
                Book = Array.Empty<Book>();
                return Page();
            }
            var data = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(data);
            Book = temp.value?.ToObject<IList<Book>>() ?? Array.Empty<Book>();
            return Page();
        }
    }
}
