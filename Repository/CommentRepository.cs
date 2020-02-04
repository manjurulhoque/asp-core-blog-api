using System;
using System.Collections.Generic;
using System.Linq;
using blogapi.Contracts;
using blogapi.Contracts.Responses;
using blogapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blogapi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Comment> FindAll()
        {
            return _context.Comments.ToList();
        }

        public CommentsByPostResponse FindAllCommentsByPost(string id)
        {
            //var post = _context.Posts.Include(x => x.Comments).FirstOrDefault(x => x.Id == id);
            // if (post == null)
            // {
            //     return new CommentsByPostResponse
            //     {
            //         Success = false
            //     };
            // }

            var comments = _context.Comments.Where(x => x.PostId == id).ToList();
            var post = comments.Last().Post;

            return new CommentsByPostResponse
            {
                Post = post,
                Comments = comments
            };
        }

        public Comment FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool isExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Create(Comment entity)
        {
            _context.Comments.Add(entity);

            return Save();
        }

        public bool Update(Comment entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var changes = _context.SaveChanges();
            return changes > 0;
        }
    }
}