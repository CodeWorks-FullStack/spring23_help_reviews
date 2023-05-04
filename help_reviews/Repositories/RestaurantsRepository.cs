namespace help_reviews.Repositories;

public class RestaurantsRepository
{
  private readonly IDbConnection _db;

  public RestaurantsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal List<Restaurant> GetAllRestaurants()
  {
    string sql = @"
    SELECT
    rest.*,
    COUNT(rep.id) AS reportCount,
    acct.*
    FROM restaurants rest
    JOIN accounts acct ON acct.id = rest.creatorId
    LEFT JOIN reports rep ON rep.restaurantId = rest.id
    GROUP BY (rest.id) 
    ;";

    List<Restaurant> restaurants = _db.Query<Restaurant, Profile, Restaurant>
    (sql, (rest, acct) =>
    {
      rest.Creator = acct;
      return rest;
    }).ToList();

    return restaurants;
  }

  internal Restaurant GetOneRestaurant(int restaurantId)
  {
    string sql = @"
    SELECT
    rest.*,
    COUNT(rep.id) AS reportCount,
    acct.*
    FROM restaurants rest
    JOIN accounts acct ON acct.id = rest.creatorId
    LEFT JOIN reports rep ON rep.restaurantId = rest.id
    WHERE rest.id = @restaurantId
    GROUP BY (rest.id);"
    ;

    Restaurant restaurant = _db.Query<Restaurant, Profile, Restaurant>
    (sql, (rest, acct) =>
    {
      rest.Creator = acct;
      return rest;
    }, new { restaurantId }).FirstOrDefault();

    return restaurant;
  }

  internal void UpdateRestaurant(Restaurant originalRestaurant)
  {
    string sql = @"
    UPDATE restaurants
    SET
    name = @Name,
    description = @Description,
    imgUrl = @ImgUrl,
    isShutdown = @IsShutdown
    WHERE id = @Id
    ;";

    _db.Execute(sql, originalRestaurant);
  }
}
