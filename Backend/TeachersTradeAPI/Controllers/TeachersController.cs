using Microsoft.AspNetCore.Mvc;
using TeachersTradeAPI.Models;

namespace TeachersTradeAPI.Controllers;

[ApiController]
[Route("API/[controller]")]
public class TeachersController : ControllerBase
{
    private ApplicationContext _db;
    public TeachersController(ApplicationContext context)
    {
        _db = context;
    }
    
    /// <summary>
    /// Get all Teachers
    /// </summary>
    /// <response code="200">Returns Json representation of Teachers table</response>
    [HttpGet]
    public IEnumerable<Teacher> GetAllTeachers()
    {
        return _db.Teachers;
    }
    
    /// <summary>
    /// Get Teacher by id
    /// </summary>
    /// <param name="id">Key value in database</param>
    /// <response code="200">Returns Json representation of one Teacher</response>
    /// <response code="404">If the Teacher is not found</response>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Teacher>> GetTeacherById(int id)
    {
         var teacher = await _db.Teachers.FindAsync(id);
         if (teacher == null)
             return NotFound();
         return teacher;
    }
    
    /// <summary>
    /// Create new Teacher
    /// </summary>
    /// <param name="newTeacher">Json representation of new Teacher without id</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /API/Teachers
    ///     {
    ///        "name": "John",
    ///        "subject": "Math",
    ///        "age": 30,
    ///        "description": "Good teacher",
    ///        "maxShares": 1000,
    ///        "price": 5
    ///     }
    /// 
    /// <b>— Price it's price per share</b>
    /// <b>— MaxShares can be null</b>
    /// </remarks>
    /// <response code="201">Returns Json representation of new Teacher with new id</response>
    /// <response code="400">Incorrectly entered parameters</response>
    [HttpPost]
    public async Task<ActionResult<Teacher>> AddTeacher(Teacher newTeacher)
    {
        // TODO: Add json validation
        newTeacher.MaxShares ??= Global.DefaultMaxShares;
        _db.Teachers.Add(newTeacher);
        await _db.SaveChangesAsync();
        return CreatedAtAction("AddTeacher", "Teachers", null, newTeacher);
    }
    
    /// <summary>
    /// Update existing Teacher
    /// </summary>
    /// <param name="updateTeacher">Json representation of new Teacher with id</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /API/Teachers
    ///     {
    ///        "id": 1,
    ///        "name": "John",
    ///        "subject": "Math",
    ///        "age": 30,
    ///        "description": "Good teacher",
    ///        "maxShares": 1000,
    ///        "price": 5
    ///     }
    /// 
    /// <b>— Price it's price per share</b>
    /// <b>— MaxShares can be null</b>
    /// </remarks>
    /// <response code="200">Returns Json representation of updated Teacher</response>
    /// <response code="404">If the Teacher is not found</response>
    /// <response code="400">Incorrectly entered parameters</response>
    [HttpPut]
    public async Task<ActionResult<Teacher>> UpdateTeacher(Teacher updateTeacher)
    {
        var teacher = await _db.Teachers.FindAsync(updateTeacher.Id);
        if (teacher == null) return NotFound();
        teacher.Name = updateTeacher.Name;
        teacher.Subject = updateTeacher.Subject;
        teacher.Age = updateTeacher.Age;
        teacher.Description = updateTeacher.Description;
        teacher.MaxShares = updateTeacher.MaxShares;
        teacher.Price = updateTeacher.Price;
        await _db.SaveChangesAsync();
        return Ok();
    }
    
    /// <summary>
    /// Delete Teacher by id
    /// </summary>
    /// <param name="id">Key value in database</param>
    /// <response code="200">Returns Json representation of deleted Teacher</response>
    /// <response code="404">If the Teacher is not found</response>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Teacher>> DeleteTeacher(int id)
    {
        var teacher = await _db.Teachers.FindAsync(id);
        if (teacher == null) return NotFound();
        _db.Teachers.Remove(teacher);
        await _db.SaveChangesAsync();
        return teacher;
    }
}