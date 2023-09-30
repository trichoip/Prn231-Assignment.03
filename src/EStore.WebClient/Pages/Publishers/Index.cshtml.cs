using EBookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace EStore.WebClient.Pages.Publishers;

public class IndexModel : PageModel
{
    private readonly string Url = "https://localhost:7089/odata/Publishers";
    private readonly HttpClient httpClient;
    public IndexModel()
    {
        httpClient = new HttpClient();
    }

    public IList<Publisher> Publisher { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        var response = await httpClient.GetAsync(Url);
        if (!response.IsSuccessStatusCode)
        {
            Publisher = Array.Empty<Publisher>();
            return Page();
        }
        var data = await response.Content.ReadAsStringAsync();
        dynamic temp = JObject.Parse(data);
        Publisher = temp.value?.ToObject<IList<Publisher>>() ?? Array.Empty<Publisher>();
        return Page();
    }
}
