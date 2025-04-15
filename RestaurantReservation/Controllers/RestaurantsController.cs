using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Common;
using RestaurantReservation.Models.Users;
using RestaurantReservation.Services;
using RestaurantReservation.Services.RestaurantsService;
using static RestaurantReservation.Common.GlobalEnums.GlobalEnums;

namespace RestaurantReservation.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantListService _restaurantsListService;
        private readonly IBaseService<Restaurants> _restaurantsService;
        public RestaurantController( RestaurantListService restaurantListService
                                   , IBaseService<Restaurants> restaurantService)
        {
            _restaurantsListService = restaurantListService;
            _restaurantsService = restaurantService;
        }

        //roles
        [HttpGet("search")]
        public async Task<IActionResult> SearchRestaurants([FromQuery] JsonElement jsonRequest)
        {
            try
            {
                var result = await _restaurantsListService.GetFilteredRestaurantsAsync(jsonRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is ErrorMsg)
                    return BadRequest(ex.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(short id)
        {
            try
            {
                var result = await _restaurantsService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is ErrorMsg)
                    return BadRequest(ex.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(short id, [FromBody] Restaurants request)
        {
            try
            {
                await _restaurantsService.UpdateAsync(id, request);
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

        [Authorize(Roles = "SystemAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            try
            {
                await _restaurantsService.DeleteAsync(id);
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
    }
}
