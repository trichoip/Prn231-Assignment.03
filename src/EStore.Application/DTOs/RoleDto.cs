using EStore.Application.Mappings;
using EStore.Domain.Entities;

namespace EStore.Application.DTOs
{
    public class RoleDto : IMapFrom<Role>
    {
        public int RoleId { get; set; }
        public string RoleDesc { get; set; }
    }
}
