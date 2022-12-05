using System.Net.Mail;
using System.Text.RegularExpressions;
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
    
    /// <summary>
    /// Get all Users
    /// </summary>
    /// <response code="200">Returns Json representation of Users table</response>
    [HttpGet]
    public IEnumerable<User> GetAllUsers()
    {
        return _db.Users;
    }
    
    /// <summary>
    /// Get User by id
    /// </summary>
    /// <param name="id">Key value in database</param>
    /// <response code="200">Returns Json representation of one User</response>
    /// <response code="404">If the User is not found</response>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
         var user = await _db.Users.FindAsync(id);
         if (user == null)
             return NotFound();
         return user;
    }
    
    /// <summary>
    /// Create new User
    /// </summary>
    /// <param name="newUser">Json representation of new User without id</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /API/Users
    ///     {
    ///        "name": "John",
    ///        "email": "example@gmail.com",
    ///        "password": "example123",
    ///        "balance": 60.5
    ///     }
    ///
    /// <b>— Role can be "user" or "admin"</b>
    /// </remarks>
    /// <response code="201">Returns Json representation of new User with new id</response>
    /// <response code="400">Incorrect data</response>
    [HttpPost]
    public async Task<ActionResult<User>> AddUser(User newUser)
    {
        newUser.Id = -1;
        if (!CheckUser(newUser, out var message))
            return BadRequest(message);
        newUser.Password = Global.CreateMd5(newUser.Password);
        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();
        return CreatedAtAction("AddUser", "Users", null, newUser);
    }
    
    /// <summary>
    /// Update existing User
    /// </summary>
    /// <param name="updateUser">Json representation of new User with id</param>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     POST /API/Users
    ///     {
    ///        "id": 1,
    ///        "name": "John",
    ///        "email": "example@gmail.com",
    ///        "password": "example123"
    ///        "balance": 60.5
    ///     }
    /// 
    /// <b>— Role can be "user" or "admin"</b>
    /// </remarks>
    /// <response code="200">Returns Json representation of updated User</response>
    /// <response code="400">Incorrect data</response>
    /// <response code="404">If the User is not found</response>
    [HttpPut]
    public async Task<ActionResult<User>> UpdateUser(User updateUser)
    {
        if (!CheckUser(updateUser, out var message))
            return BadRequest(message);
        var user = await _db.Users.FindAsync(updateUser.Id);
        if (user == null) return NotFound();
        
        user.Name = updateUser.Name;
        user.Email = updateUser.Email;
        
        if (updateUser.Password != null) 
            updateUser.Password = Global.CreateMd5(updateUser.Password);
        
        user.Password = updateUser.Password;
        user.Role = updateUser.Role;
        user.Balance = updateUser.Balance;
        
        await _db.SaveChangesAsync();
        return user;
    }

    /// <summary>
    /// Delete User by id
    /// </summary>
    /// <param name="id">Key value in database</param>
    /// <response code="200">Returns Json representation of deleted User</response>
    /// <response code="404">If the User is not found</response>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound();
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return user;
    }
    
    private bool CheckUser(User user, out string message)
    {
        message = string.Empty;
        var result = true;
        // Name validation
        if (user.Name == null || user.Name.Length < 3 || _db.Users.FirstOrDefault(u => u.Name == user.Name && u.Id != user.Id) != null)
        {
            message += "Incorrect name.\n";
            result = false;
        }

        // Email validation
        try
        {
            var m = new MailAddress(user.Email!);
        }
        catch (Exception)
        {
            message += "Incorrect email form.\n";
            result =  false;
        }
        
        // Password validation
        var hasNumber = new Regex(@"[0-9]+");
        var hasEnglishChars = new Regex(@"[a-zA-Z]+");
        var hasMinimum8Chars = new Regex(@".{8,}");

        if (user.Password == null || !hasNumber.IsMatch(user.Password) || !hasEnglishChars.IsMatch(user.Password) ||
            !hasMinimum8Chars.IsMatch(user.Password))
        {
            message += "Incorrect password form.\n";
            result =  false;
        }
            

        // Role validation
        if (user.Role is not ("admin" or "user"))
        {
            user.Role = "user";
        }

        // Balance validation
        if (user.Balance is < 0 or > 1000000)
        {
            message += "Incorrect balance.";
            result = false;
        }

        return result;
    }
}