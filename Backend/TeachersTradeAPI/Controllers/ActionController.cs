using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachersTradeAPI.Models;

namespace TeachersTradeAPI.Controllers;

[ApiController]
[Route("API/[controller]")]
public class ActionController : ControllerBase
{
    private ApplicationContext _db;
    public ActionController(ApplicationContext context)
    {
        _db = context;
    }
    
    /// <summary>
    /// Buy shares of a certain Teacher
    /// </summary>
    /// <param name="userId">Id of the User</param>
    /// <param name="teacherId">Id of the Teacher whose shares the user wants to buy</param>
    /// <param name="shareCount">Count of Shares that the user wants to buy</param>
    /// <response code="200">Returns Json representation of created or changed Share</response>
    /// <response code="400"><p>1. If the shareCount is less than 1</p><p>2. If the user does not have enough money</p></response>
    /// <response code="404">If the user or Teacher is not found</response>
    /// <response code="409">If the market has run out of Teacher shares</response>
    [HttpPost("Buy/")]
    public async Task<ActionResult<Share>> BuyTeacher(int userId, int teacherId, int shareCount)
    {
        var user = await _db.Users.FindAsync(userId);
        var teacher = await _db.Teachers.FindAsync(teacherId);
        
        if (user == null || teacher == null)
            return NotFound();

        var countOfBoughtShares = await _db.Shares.Where(s => s.TeacherId == teacherId).Select(s => s.Value).SumAsync();
        
        if (countOfBoughtShares + shareCount > teacher.MaxShares)
            return Conflict("Too many shares");
        
        if (shareCount <= 0)
            return BadRequest("Invalid price");
        
        if (user.Balance < teacher.Price * shareCount)
            return BadRequest("Not enough money");

        user.Balance -= teacher.Price * shareCount;
        var share = await _db.Shares.FindAsync(userId, teacherId);
        if (share == null)
        {
            share = new Share
            {
                UserId = userId,
                TeacherId = teacherId,
                Value = shareCount
            };
            await _db.Shares.AddAsync(share);
        }
        else
        {
            share.Value += shareCount;
        }
        await _db.SaveChangesAsync();
        return share;
    }

    /// <summary>
    /// Sell shares of a certain Teacher
    /// </summary>
    /// <param name="userId">Id of the User</param>
    /// <param name="teacherId">Id of the purchased Teacher</param>
    /// <param name="shareCount">Count of Shares that the user wants to sell</param>
    /// <response code="200">Returns Json representation of deleted or changed Share</response>
    /// <response code="400">If the shareCount is less than 1</response>
    /// <response code="404">If the User or Teacher is not found</response>
    [HttpPost("Sell/")]
    public async Task<ActionResult<Share>> SellTeacher(int userId, int teacherId, int shareCount)
    {
        var user = await _db.Users.FindAsync(userId);
        var teacher = await _db.Teachers.FindAsync(teacherId);
        
        if (user == null || teacher == null)
            return NotFound();
        
        if (shareCount <= 0)
            return BadRequest("Invalid price");
        
        var share = await _db.Shares.FindAsync(userId, teacherId);
        if (share == null || share.Value < shareCount)
            return BadRequest("Not enough shares");
        
        user.Balance += teacher.Price * shareCount;
        share.Value -= shareCount;
        if (share.Value == 0)
            _db.Shares.Remove(share);
        await _db.SaveChangesAsync();
        return share;
    }
    
    // TODO: Add balance change method
}