﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using blogapi.Contracts;
using blogapi.Contracts.Requests;
using blogapi.Extensions;
using blogapi.Models;
using blogapi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blogapi.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repo;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository repo, IMapper mapper, IUserRepository userRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // GET api/post
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<PostViewModel> GetPosts()
        {
            var posts = _repo.FindAll().ToList();
            var model = _mapper.Map<List<Post>, List<PostViewModel>>(posts);

            return model;
        }

        // POST api/post
        [HttpPost]
        public CreatePostRequest AddPost([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post
            {
                Title = postRequest.Title,
                Description = postRequest.Description,
                UserId = HttpContext.GetCurrentUserId()
            };
            _repo.Create(post);
            // var email = User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            //var id = HttpContext.User.Claims.FirstOrDefault().Value;
            //var user =  _authBusiness.GetUser(email);
            //var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            //System.Diagnostics.Debug.WriteLine(id);
            //var user = GetSecureUser();
            return postRequest;
        }

        [AllowAnonymous]
        // GET api/post/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Post GetPost(Guid id)
        {
            return _repo.FindById(id);
        }

        [HttpPut("{id}")]
        public ActionResult<Post> UpdatePost(Guid id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _repo.Update(post);
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(Guid id)
        {
            _repo.Delete(id);
            return Ok();
        }
    }
}