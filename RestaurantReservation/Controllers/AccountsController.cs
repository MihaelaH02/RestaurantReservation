using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Common;
using RestaurantReservation.Services.Users;
using System.Text.Json;

namespace RestaurantReservation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountsService _accountService;
        private readonly PasswordResetService _passwordResetService;

        public AccountController( AccountsService accountService
                                , PasswordResetService passwordResetService)
        {
            _accountService = accountService;
            _passwordResetService = passwordResetService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] JsonElement registerRequest)
        {
            try
            {
                var tokenResponse = await _accountService.Register(registerRequest);
                return Ok(new { Token = tokenResponse });
            }
            catch (Exception ex)
            {
                if (ex is ErrorMsg)
                    return BadRequest(ex.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] JsonElement jsonRequest)
        {
            try
            {
                var tokenResponse = await _accountService.Login(jsonRequest);
                return Ok(new { Token = tokenResponse });
            }
            catch (Exception ex)
            {
                if (ex is ErrorMsg)
                    return BadRequest(ex.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("password-reset-request")]
        public async Task<IActionResult> RequestReset([FromBody] string emailrequest)
        {
            try
            {
                await _passwordResetService.SendResetCodeAsync(emailrequest);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is ErrorMsg)
                    return BadRequest(ex.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("password-reset-confirm")]
        public async Task<IActionResult> ConfirmReset([FromBody] JsonElement jsonRequest)
        {
            try
            {
                var tokenResponse = _passwordResetService.ResetPasswordAsync(jsonRequest);
                return Ok(new { Token = tokenResponse });
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
