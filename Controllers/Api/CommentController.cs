using System;
using AutoMapper;
using blogapi.Contracts;
using blogapi.Contracts.Requests;
using blogapi.Contracts.Responses;
using blogapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace blogapi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repo;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository repo, IMapper mapper, IUserRepository userRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("posts/{id}")]
        public CommentsByPostResponse FindAllCommentsByPost(string id)
        {
            return _repo.FindAllCommentsByPost(id);
        }

        // POST api/post
        [HttpPost("posts/{id}")]
        public DefaultResult AddComment(string id, [FromBody] CreateCommentRequest commentRequest)
        {
            var comment = new Comment
            {
                Content = commentRequest.Content,
                PostId = id
            };
            _repo.Create(comment);
            // var email = User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            //var id = HttpContext.User.Claims.FirstOrDefault().Value;
            //var user =  _authBusiness.GetUser(email);
            //var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            //System.Diagnostics.Debug.WriteLine(id);
            //var user = GetSecureUser();
            return new DefaultResult
            {
                Success = true,
                Message = "Comment successfully created"
            };
        }
    }
}