using Microsoft.AspNetCore.Mvc;
using TeachersTradeAPI.Models;

namespace TeachersTradeAPI.Controllers;

[ApiController]
[Route("API/[controller]")]
public class SharesController : ControllerBase
{
    private ApplicationContext _db;
    
    public SharesController(ApplicationContext context)
    {
        _db = context;
    }
    // TODO: Add more methods here
    /// <summary>
    /// Get all Shares
    /// </summary>
    /// <response code="200">Returns Json representation of Shares table</response>
    [HttpGet]
    public IEnumerable<Share> GetAllShares()
    {
        return _db.Shares;
    }
    
    /// <summary>
    /// Get Share by User and Teacher id
    /// </summary>
    /// <param name="userId">Id of the User</param>
    /// <param name="teacherId">Id of the purchased Teacher</param>
    /// <response code="200">Returns Json representation of one Share</response>
    /// <response code="404">If the Share is not found</response>
    [HttpGet("{userId:int}/{teacherId:int}")]
    public async Task<ActionResult<Share>> GetShare(int userId, int teacherId)
    {
        var share = await _db.Shares.FindAsync(userId, teacherId);
        if (share == null)
            return NotFound();
        return share;
    }

}