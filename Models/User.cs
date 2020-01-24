using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace blogapi.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(10)] public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<Post> Posts { get; set; }
    }
}