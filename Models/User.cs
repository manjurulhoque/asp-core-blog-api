using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace blogapi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [EmailAddress] public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<Post> Posts { get; set; }
    }
}