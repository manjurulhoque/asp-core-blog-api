using System.ComponentModel.DataAnnotations;

namespace blogapi.Contracts.Requests
{
    public class CreateCommentRequest
    {
        [Required]
        public string Content { get; set; }
    }
}