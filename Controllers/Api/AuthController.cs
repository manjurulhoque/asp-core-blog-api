using System.Linq;
using AutoMapper;
using blogapi.Contracts;
using System.Threading.Tasks;
using blogapi.Contracts.Requests;
using blogapi.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

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
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            
            var authResponse = await _userRepository.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
            });
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _userRepository.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
            });
        }
    }
}