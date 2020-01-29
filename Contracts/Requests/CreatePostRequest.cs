using System;
using System.ComponentModel.DataAnnotations;

namespace blogapi.Contracts.Requests
{
    public class CreatePostRequest
    {
        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}