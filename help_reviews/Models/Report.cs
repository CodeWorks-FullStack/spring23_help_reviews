using System.ComponentModel.DataAnnotations;

namespace help_reviews.Models;

public class Report : RepoItem<int>
{
  public string Title { get; set; }
  public string Body { get; set; }
  [Range(1, 5)]
  // NOTE                         VVV Default value
  public int Rating { get; set; } = 1;
  public int RestaurantId { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}
