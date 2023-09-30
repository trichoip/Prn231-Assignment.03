﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EStore.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Source { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? HireDate { get; set; }
        public int? RoleId { get; set; }
        public int? PubId { get; set; }
        [ForeignKey("PubId")]
        public virtual Publisher? Publisher { get; set; }
        public virtual Role? Role { get; set; }
    }
}
