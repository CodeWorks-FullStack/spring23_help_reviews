namespace help_reviews.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
  private readonly RestaurantsService _restaurantsService;
  private readonly Auth0Provider _auth;

  public RestaurantsController(RestaurantsService restaurantsService, Auth0Provider auth)
  {
    _restaurantsService = restaurantsService;
    _auth = auth;
  }

  [HttpGet]
  public async Task<ActionResult<List<Restaurant>>> GetAllRestaurants()
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<Restaurant> restaurants = _restaurantsService.GetAllRestaurants(userInfo?.Id);
      return Ok(restaurants);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{restaurantId}")]
  public async Task<ActionResult<Restaurant>> GetOneRestaurant(int restaurantId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Restaurant restaurant = _restaurantsService.GetOneRestaurant(restaurantId, userInfo?.Id);
      return Ok(restaurant);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{restaurantId}")]
  [Authorize]
  public async Task<ActionResult<Restaurant>> UpdateRestaurant(int restaurantId, [FromBody] Restaurant restaurantData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      restaurantData.Id = restaurantId;
      restaurantData.CreatorId = userInfo.Id;
      Restaurant restaurant = _restaurantsService.UpdateRestaurant(restaurantData);
      return Ok(restaurant);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
