namespace help_reviews.Repositories;

public class ReportsRepository
{
  private readonly IDbConnection _db;

  public ReportsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Report CreateReport(Report reportData)
  {
    string sql = @"
    INSERT INTO reports(title, body, rating, restaurantId, creatorId)
    VALUES(@Title, @Body, @Rating, @RestaurantId, @CreatorId);
    SELECT LAST_INSERT_ID()
    ;";

    int id = _db.ExecuteScalar<int>(sql, reportData);
    reportData.Id = id;
    return reportData;
  }

  internal Report GetOneReport(int id)
  {
    string sql = "SELECT * FROM reports WHERE id = @id;";

    Report report = _db.Query<Report>(sql, new { id }).FirstOrDefault();
    return report;
  }

  internal List<Report> GetReportsForRestaurant(int restaurantId)
  {
    string sql = @"
    SELECT
    rep.*,
    acct.*
    FROM reports rep
    JOIN accounts acct ON acct.id = rep.creatorId
    WHERE rep.restaurantId = @restaurantId
    ;";

    List<Report> reports = _db.Query<Report, Profile, Report>
    (sql, (rep, acct) =>
    {
      rep.Creator = acct;
      return rep;
    }, new { restaurantId }).ToList();

    return reports;
  }

  internal void RemoveReport(int reportId)
  {
    string sql = "DELETE FROM reports WHERE id = @reportId LIMIT 1;";

    _db.Execute(sql, new { reportId });
  }
}
