namespace help_reviews.Models;

public class Restaurant : RepoItem<int>
{
  public string Name { get; set; }
  public string ImgUrl { get; set; }

  public string Description { get; set; }
  public bool? IsShutdown { get; set; }
  public string CreatorId { get; set; }
  public int reportCount { get; set; } = 0;
  public Profile Creator { get; set; }
}
