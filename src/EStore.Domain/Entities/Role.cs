namespace EStore.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleDesc { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
