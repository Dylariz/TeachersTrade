using Microsoft.AspNetCore.Mvc;
using TeachersTradeAPI.Models;

namespace TeachersTradeAPI.Controllers;

[ApiController]
[Route("API/[controller]")]
public class UsersController : ControllerBase
{
    private ApplicationContext _db;
    
    public UsersController(ApplicationContext context)
    {
        _db = context;
    }
    
    [HttpGet]
    public IEnumerable<User> GetAllUsers()
    {
        return _db.Users;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
         var user = await _db.Users.FindAsync(id);
         if (user != null)
         {
             return user;
         }

         return new NotFoundResult();
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> AddUser(User newUser)
    {
        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();
        return new CreatedAtActionResult("AddUser", "Users", null, newUser);
    }
    
    [HttpPut]
    public async Task<ActionResult<User>> UpdateUser(User updateUser)
    {
        var user = await _db.Users.FindAsync(updateUser.Id);
        if (user == null) return new NotFoundResult();
        user.Name = updateUser.Name;
        user.Email = updateUser.Email;
        user.Password = updateUser.Password;
        await _db.SaveChangesAsync();
        return new OkResult();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return new NotFoundResult();
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return new OkResult();
    }
}