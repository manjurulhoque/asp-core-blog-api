using blogapi.Contracts;
using blogapi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogapi.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Post> FindAll()
        {
            return _context.Posts.ToList();
        }

        public bool Create(Post entity)
        {
            _context.Posts.Add(entity);

            return Save();
        }

        public bool Delete(Guid id)
        {
            var post = FindById(id);
            _context.Posts.Remove(post);

            return Save();
        }

        public Post FindById(Guid id)
        {
            return _context.Posts.FirstOrDefault(x => x.Id == id);
        }

        public bool isExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var changes = _context.SaveChanges();
            return changes > 0;
        }

        public bool Update(Post entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //_context.Posts.Update(entity);

            return Save();
        }
    }
}
