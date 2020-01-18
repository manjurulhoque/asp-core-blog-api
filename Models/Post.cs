using System.ComponentModel.DataAnnotations;

namespace blogapi.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(255), Display(Name = "Post Title", Prompt = "Post Title")]
        public string Title { get; set; }
        [Required, Display(Name = "Post Description", Prompt = "Post Description")]
        public string Description { get; set; }
    }
}
