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
    /// Get all teachers
    /// </summary>
    /// <response code="200">Returns Json representation of Teachers table</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Teacher> GetAllTeachers()
    {
        return _db.Teachers;
    }
    
    /// <summary>
    /// Get teacher by id
    /// </summary>
    /// <param name="id">Key value in database</param>
    /// <response code="200">Returns Json representation of one Teacher</response>
    /// <response code="404">If the teacher is not found</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Teacher>> GetTeacherById(int id)
    {
         var teacher = await _db.Teachers.FindAsync(id);
         if (teacher != null)
         {
             return teacher;
         }

         return new NotFoundResult();
    }
    
    /// <summary>
    /// Create new teacher
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
    ///        "price": 300.53
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns Json representation of new Teacher with new id</response>
    /// <response code="400">Incorrectly entered parameters</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Teacher>> AddTeacher(Teacher newTeacher)
    {
        // TODO: Add json validation
        _db.Teachers.Add(newTeacher);
        await _db.SaveChangesAsync();
        return new CreatedAtActionResult("AddTeacher", "Teachers", null, newTeacher);
    }
    
    /// <summary>
    /// Update existing teacher
    /// </summary>
    /// <param name="updateTeacher">Json representation of new Teacher with id</param>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     POST /API/Teachers
    ///     {
    ///        "id": 1,
    ///        "name": "John",
    ///        "subject": "Math",
    ///        "age": 30,
    ///        "description": "Good teacher",
    ///        "price": 300.53
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns Json representation of updated Teacher</response>
    /// <response code="404">If the teacher is not found</response>
    /// <response code="400">Incorrectly entered parameters</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Teacher>> UpdateTeacher(Teacher updateTeacher)
    {
        var teacher = await _db.Teachers.FindAsync(updateTeacher.Id);
        if (teacher == null) return new NotFoundResult();
        teacher.Name = updateTeacher.Name;
        teacher.Subject = updateTeacher.Subject;
        teacher.Age = updateTeacher.Age;
        teacher.Description = updateTeacher.Description;
        teacher.Price = updateTeacher.Price;
        await _db.SaveChangesAsync();
        return new OkResult();
    }
    
    /// <summary>
    /// Delete teacher by id
    /// </summary>
    /// <param name="id">Key value in database</param>
    /// <response code="200">Returns Json representation of deleted Teacher</response>
    /// <response code="404">If the teacher is not found</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Teacher>> DeleteTeacher(int id)
    {
        var teacher = await _db.Teachers.FindAsync(id);
        if (teacher == null) return new NotFoundResult();
        _db.Teachers.Remove(teacher);
        await _db.SaveChangesAsync();
        return teacher;
    }
}