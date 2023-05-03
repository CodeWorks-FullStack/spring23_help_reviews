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

    List<Restaurant> filteredRestaurants = restaurants.FindAll(r => r.IsShutdown == false || r.CreatorId == userId);

    return filteredRestaurants;
  }

  internal Restaurant GetOneRestaurant(int restaurantId, string userId)
  {
    Restaurant restaurant = _repo.GetOneRestaurant(restaurantId);

    if (restaurant == null)
    {
      throw new Exception($"THIS ID WAS INVALID: {restaurantId}");
    }

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
