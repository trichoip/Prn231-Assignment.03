using EStore.Application.Mappings;
using EStore.Domain.Entities;

namespace EStore.Application.DTOs;
public class ApplicationUserDto : IMapFrom<ApplicationUser>
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? NormalizedUserName { get; set; }
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
