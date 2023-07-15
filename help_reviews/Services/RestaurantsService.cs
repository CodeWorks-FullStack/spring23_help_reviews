namespace help_reviews.Services;

public class RestaurantsService
{
  private readonly RestaurantsRepository _repo;

  public RestaurantsService(RestaurantsRepository repo)
  {
    _repo = repo;
  }

  internal List<Restaurant> GetAllRestaurants(string userId)
  {
    List<Restaurant> restaurants = _repo.GetAllRestaurants();



    // NOTE FindAll works very similar to a filter in javascript. On this line, we only keep the restaurant in our new list if it isn't closed down OR if the user making the request is the owner of the restaurant. If the user is not logged in, r.creatorId will never equal null, so we only send back open restaurants to the user.

    List<Restaurant> filteredRestaurants = restaurants.FindAll(restaurant => restaurant.IsShutdown == false || restaurant.CreatorId == userId);

    return filteredRestaurants;
  }

  internal Restaurant GetOneRestaurant(int restaurantId, string userId)
  {
    Restaurant restaurant = _repo.GetOneRestaurant(restaurantId);

    if (restaurant == null)
    {
      throw new Exception($"THIS ID WAS INVALID: {restaurantId}");
    }

    // NOTE additional check here to make sure we are still returning sensitive data to the appropriate users.
    // NOTE **remember that the creator of a shutdown restaurant should still be able to retrieve and view it.

    if (restaurant.IsShutdown == true && userId != restaurant.CreatorId)
    {
      throw new Exception("SOMETHING WENT WRONG 😉");
    }

    return restaurant;
  }

  internal Restaurant UpdateRestaurant(Restaurant restaurantData)
  {
    Restaurant originalRestaurant = GetOneRestaurant(restaurantData.Id, restaurantData.CreatorId);

    if (originalRestaurant.CreatorId != restaurantData.CreatorId)
    {
      throw new Exception("THAT AIN'T YOUR RESTAURANT, BUD");
    }

    originalRestaurant.Name = restaurantData.Name ?? originalRestaurant.Name;
    originalRestaurant.Description = restaurantData.Description ?? originalRestaurant.Description;
    originalRestaurant.ImgUrl = restaurantData.ImgUrl ?? originalRestaurant.ImgUrl;
    originalRestaurant.IsShutdown = restaurantData.IsShutdown != null ? restaurantData.IsShutdown : originalRestaurant.IsShutdown;

    _repo.UpdateRestaurant(originalRestaurant);

    return originalRestaurant;
  }
}
