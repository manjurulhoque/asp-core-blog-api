using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using blogapi.Contracts;
using blogapi.Models;
using blogapi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blogapi.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repo;
        private readonly IMapper _mapper;
        public PostController(IPostRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET api/post
        [HttpGet]
        public IEnumerable<PostViewModel> GetPosts()
        {
            var posts = _repo.FindAll().ToList();
            var model = _mapper.Map<List<Post>, List<PostViewModel>>(posts);

            return model;
        }

        // POST api/post
        [HttpPost]
        public ActionResult<PostViewModel> AddPost([FromBody]Post post)
        {
            _repo.Create(post);
            return Ok(post);
        }

        // GET api/post/{id}
        [HttpGet("{id}")]
        public Post GetPost(int id)
        {
            return _repo.FindById(id);
        }

        [HttpPut("{id}")]
        public ActionResult<Post> UpdatePost(int id, [FromBody]Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }
            _repo.Update(post);
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public IActionResult DetePost(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid post id");
            _repo.Delete(id);
            return Ok();
        }
    }
}