using System;
using System.ComponentModel.DataAnnotations;

namespace blogapi.Models
{
    public class Comment
    {
        [Key] public Guid Id { get; set; }
        [Required, MaxLength(255)]
        public string Content { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }
    }
}