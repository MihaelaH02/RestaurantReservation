using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Common;
using RestaurantReservation.DTO.RegisterDTO;
using RestaurantReservation.Models.Users;
using RestaurantReservation.Services.Users;
using System.Text.Json;

namespace RestaurantReservation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountsService _accountService;

        public AccountController(AccountsService accountService, JwtService jwtService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] JsonElement registerRequest)
        {
            try
            {
                var token = await _accountService.Register(registerRequest);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                if (ex is ErrorMsg)
                    return BadRequest(ex.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromBody] JsonElement loginRequest)
        {
            try
            {
                var token = await _accountService.Login(loginRequest);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                if (ex is ErrorMsg)
                    return BadRequest(ex.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
