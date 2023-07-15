namespace help_reviews.Controllers;



[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
  private readonly RestaurantsService _restaurantsService;
  private readonly ReportsService _reportsService;
  private readonly Auth0Provider _auth;

  public RestaurantsController(RestaurantsService restaurantsService, Auth0Provider auth, ReportsService reportsService)
  {
    _restaurantsService = restaurantsService;
    _auth = auth;
    _reportsService = reportsService;
  }

  // NOTE In this app, we consider a closed-down restaurant to be sensitive information. Every user (authenticated or not) should be able to see the data for a restaurant that is open. However, we don't want every user to see the data for a closed-down restaurant. Only the owner of a closed-down restaurant should be able to view that data.

  [HttpGet]
  public async Task<ActionResult<List<Restaurant>>> GetAllRestaurants()
  {
    try
    {
      // Even though this route is not locked with an 'Authorize' we can still check to see if the user who made this request is logged in, and let our service handle this appropriately.
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);

      // NOTE If the user is not logged in, here userInfo will be null so we add the '?' elvis operator so that our code does not break.
      // userInfo?.id says DO NOT drill into my userInfo object if it is null. This is necessary for when I may need to check if the user logged in has certain access to data/can perform certain operations. Sometimes the person making this request has the rights to see/manipulate data, but not in ALL cases, so we must allow this property to be nullable.

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

  [HttpGet("{restaurantId}/reports")]
  public async Task<ActionResult<List<Report>>> GetReportsForRestaurant(int restaurantId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<Report> reports = _reportsService.GetReportsForRestaurant(restaurantId, userInfo?.Id);
      return Ok(reports);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}
