using Microsoft.AspNetCore.Identity;

namespace EStore.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
