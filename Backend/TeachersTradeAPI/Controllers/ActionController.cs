using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using TeachersTradeAPI.Models;

namespace TeachersTradeAPI.Controllers;

[ApiController]
[Route("API/[controller]")]
[SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
public class ActionController : ControllerBase
{
    private ApplicationContext _db;
    
    public ActionController(ApplicationContext context)
    {
        _db = context;
    }
    
    [HttpPost("{userId:int}/buy/{teacherId:int}")]
    public async Task<ActionResult<User>> BuyTeacher(int userId, int teacherId)
    {
        _db.Teachers.ToList();
        var user = await _db.Users.FindAsync(userId);
        var teacher = await _db.Teachers.FindAsync(teacherId);
        if (user == null || teacher == null) return new NotFoundResult();
        if (user.Balance < teacher.Price || (user.PurchasedTeachers != null && user.PurchasedTeachers.Contains(teacher))) return new BadRequestResult();
        user.Balance -= teacher.Price;
        if (user.PurchasedTeachers == null) user.PurchasedTeachers = new List<Teacher> { teacher };
        else
            user.PurchasedTeachers.Add(teacher);
        await _db.SaveChangesAsync();
        return new OkResult();
    }

    [HttpPost("{userId:int}/sell/{teacherId:int}")]
    public async Task<ActionResult<User>> SellTeacher(int userId, int teacherId)
    {
        _db.Teachers.ToList();
        var user = await _db.Users.FindAsync(userId);
        var teacher = await _db.Teachers.FindAsync(teacherId);
        if (user == null || teacher == null) return new NotFoundResult();
        if (user.PurchasedTeachers == null || !user.PurchasedTeachers.Contains(teacher)) return new BadRequestResult();
        user.Balance += teacher.Price;
        user.PurchasedTeachers?.Remove(teacher);
        await _db.SaveChangesAsync();
        return new OkResult();
    }
}