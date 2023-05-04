namespace help_reviews.Models;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
  private readonly ReportsService _reportsService;
  private readonly Auth0Provider _auth;

  public ReportsController(ReportsService reportsService, Auth0Provider auth)
  {
    _reportsService = reportsService;
    _auth = auth;
  }

  [HttpPost]
  public async Task<ActionResult<Report>> CreateReport([FromBody] Report reportData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      reportData.CreatorId = userInfo.Id;
      Report report = _reportsService.CreateReport(reportData);
      report.Creator = userInfo;
      return Ok(report);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{reportId}")]
  public async Task<ActionResult<string>> RemoveReport(int reportId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _reportsService.RemoveReport(reportId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
