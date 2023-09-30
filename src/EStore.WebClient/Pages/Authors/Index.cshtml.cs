using EStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EStore.WebClient.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly string Url = "https://localhost:7089/odata/Authors";
        private readonly HttpClient httpClient;
        public IndexModel()
        {
            httpClient = new HttpClient();
        }

        public IList<Author> Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await httpClient.GetAsync(Url);
            if (!response.IsSuccessStatusCode)
            {
                Author = Array.Empty<Author>();
                return Page();
            }
            var data = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(data);
            Author = temp.value?.ToObject<IList<Author>>() ?? Array.Empty<Author>();
            return Page();
        }
    }
}
