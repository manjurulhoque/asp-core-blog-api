using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blogapi.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        [Required, MaxLength(255), Display(Name = "Post Title", Prompt = "Post Title")]
        public string Title { get; set; }
        [Required, Display(Name = "Post Description", Prompt = "Post Description")]
        public string Description { get; set; }
    }
}
