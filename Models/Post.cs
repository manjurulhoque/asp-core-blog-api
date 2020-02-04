using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using blogapi.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace blogapi.Models
{
    public class Post
    {
        [Key] public Guid Id { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UserId { get; set; }
        
        public IdentityUser User { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
    }
}