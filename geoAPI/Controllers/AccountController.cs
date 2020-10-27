using System.Net.Http;
using System.Threading.Tasks;
using geoAPI.App.Repositories.Interfaces;
using geoAPI.Models.Entities;
using geoAPI.Models.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace geoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AccountController(SignInManager<User> signInManager, IConfiguration configuration, IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseVM>> Login([FromBody] LoginVM login)
        {
            // make sure the model has all required data
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            // Validate user login
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true).ConfigureAwait(false);

            if (result.IsLockedOut)
            {
                return BadRequest("This user account has been locked out, please try again later");
            }
            else if (!result.Succeeded)
            {
                return BadRequest("Invalid username/password");
            }

            // get the user profile
            var user = await _userRepository.GetUserByEmail(login.Email);

            // get a token fron the identity server
            using (var httpClient = new HttpClient())
            {
                var authority = _configuration.GetSection("Identity").GetValue<string>("Authority");

                // Make the call to our Identity Server
                var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = authority + "/connect/token",
                    UserName = login.Email,
                    Password = login.Password,
                    ClientId = login.ClientId,
                    ClientSecret = "EcBSm40SIOmHthHZZTSfWyZjAC2Y7vws",
                    Scope = "geoapi.scope"
                }).ConfigureAwait(false);

                if (tokenResponse.IsError)
                {
                    return BadRequest("Unable to grant access to user account");
                }

                return Ok(new LoginResponseVM(tokenResponse, user));

            }
        }
    }
}
