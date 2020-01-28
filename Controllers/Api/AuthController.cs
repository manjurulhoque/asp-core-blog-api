using System;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using blogapi.Contracts;
using blogapi.Helpers;
using blogapi.Models;
using blogapi.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace blogapi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        //private readonly UserManager<User> _userManager;

        public AuthController(IUserRepository repo, IMapper mapper)
        {
            _userRepository = repo;
            _mapper = mapper;
            //_userManager = userManager;
        }

        // POST /api/auth/register
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                _userRepository.Register(user, model.Password);
                return Ok(user);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] RegisterModel model)
        {
            var user = _userRepository.Authenticate(model.Email, model.Password);
//
//            if (user == null)
//                return BadRequest(new {message = "Email or password is incorrect"});
//
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes("Secretttpassword");
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                    new Claim(ClaimTypes.Email, user.Email)
//                }),
//                Expires = DateTime.UtcNow.AddDays(1),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
//                    SecurityAlgorithms.HmacSha256Signature)
//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            var tokenString = tokenHandler.WriteToken(token);

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret key"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddDays(1));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new
            {
                Id = user.Id,
                Username = user.Email,
                Token = encodedJwt
            });

            // return basic user info and authentication token
//            return Ok(new
//            {
//                Id = user.Id,
//                Username = user.Email,
//                Token = tokenString
//            });
        }
    }
}