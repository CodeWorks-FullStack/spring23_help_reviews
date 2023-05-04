namespace help_reviews.Services;

public class ReportsService
{
  private readonly ReportsRepository _repo;
  private readonly RestaurantsService _restaurantsService;

  public ReportsService(ReportsRepository repo, RestaurantsService restaurantsService)
  {
    _repo = repo;
    _restaurantsService = restaurantsService;
  }

  internal Report CreateReport(Report reportData)
  {
    Report report = _repo.CreateReport(reportData);
    report.CreatedAt = DateTime.Now;
    report.UpdatedAt = DateTime.Now;
    return report;
  }

  internal Report GetOneReport(int id)
  {
    Report report = _repo.GetOneReport(id);
    if (report == null)
    {
      throw new Exception($"INVALID ID: {id}");
    }
    return report;
  }

  internal List<Report> GetReportsForRestaurant(int restaurantId, string userId)
  {
    // NOTE running this to verify if the user is the owner of the restuarant or if it's shutdown etc etc
    _restaurantsService.GetOneRestaurant(restaurantId, userId);

    List<Report> reports = _repo.GetReportsForRestaurant(restaurantId);
    return reports;
  }

  internal string RemoveReport(int reportId, string userId)
  {
    Report report = GetOneReport(reportId);

    if (report.CreatorId != userId)
    {
      throw new Exception("Something went wrong 😏");
    }

    _repo.RemoveReport(reportId);

    return $"The report that you wrote with the title of {report.Title} has been removed.";
  }
}
