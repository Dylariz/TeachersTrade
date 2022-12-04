namespace TeachersTradeAPI.Models;

public class Teacher
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Subject { get; set; }
    public int? Age { get; set; }
    public string? Description { get; set; }
    public int? ShareCount { get; set; }
    public double? Price { get; set; }
}