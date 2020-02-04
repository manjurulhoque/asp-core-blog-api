using System;
using System.Collections.Generic;
using blogapi.Contracts.Responses;
using blogapi.Models;

namespace blogapi.Contracts
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        CommentsByPostResponse FindAllCommentsByPost(string id);
    }
}