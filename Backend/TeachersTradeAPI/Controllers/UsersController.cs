using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using TeachersTradeAPI.Models;

namespace TeachersTradeAPI.Controllers;

[ApiController]
[Route("API/[controller]")]
[SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
public class UsersController : ControllerBase
{
    private ApplicationContext _db;

    public UsersController(ApplicationContext context)
    {
        _db = context;
    }
    
    /// <summary>
    /// Get all users
    /// </summary>
    /// <response code="200">Returns Json representation of Users table</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<User> GetAllUsers()
    {
        _db.Teachers.ToList();
        return _db.Users;
    }
    
    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id">Key value in database</param>
    /// <response code="200">Returns Json representation of one User</response>
    /// <response code="404">If the user is not found</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        _db.Teachers.ToList();
         var user = await _db.Users.FindAsync(id);
         if (user != null)
         {
             return user;
         }

         return new NotFoundResult();
    }
    
    /// <summary>
    /// Create new user
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
    ///        "role": "student",
    ///        "balance": 60.5
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns Json representation of new User with new id</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<User>> AddUser(User newUser)
    {
        // TODO: Add json validation
        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();
        return new CreatedAtActionResult("AddUser", "Users", null, newUser);
    }
    
    /// <summary>
    /// Update existing user
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
    ///        "role": "student",
    ///        "balance": 60.5
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns Json representation of updated User</response>
    /// <response code="404">If the user is not found</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> UpdateUser(User updateUser)
    {
        var user = await _db.Users.FindAsync(updateUser.Id);
        if (user == null) return new NotFoundResult();
        user.Name = updateUser.Name;
        user.Email = updateUser.Email;
        user.Password = updateUser.Password;
        user.Role = updateUser.Role;
        user.Balance = updateUser.Balance;
        await _db.SaveChangesAsync();
        return new OkResult();
    }

    /// <summary>
    /// Delete user by id
    /// </summary>
    /// <param name="id">Key value in database</param>
    /// <response code="200">Returns Json representation of deleted User</response>
    /// <response code="404">If the user is not found</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return new NotFoundResult();
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return user;
    }
}