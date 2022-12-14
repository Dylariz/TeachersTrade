namespace TeachersTradeAPI.Models;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
    public double? Balance { get; set; }
    public List<Teacher>? PurchasedTeachers { get; set; }
}