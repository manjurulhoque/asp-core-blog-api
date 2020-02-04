using System.Collections.Generic;
using blogapi.Models;

namespace blogapi.Contracts.Responses
{
    public class CommentsByPostResponse
    {
        public Post Post { get; set; }
        
        public IEnumerable<Comment> Comments { get; set; }
    }
}