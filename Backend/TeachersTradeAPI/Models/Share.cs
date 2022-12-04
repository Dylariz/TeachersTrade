using Microsoft.EntityFrameworkCore;

namespace TeachersTradeAPI.Models;

[PrimaryKey(nameof(UserId), nameof(TeacherId))]
public class Share
{
    public int UserId { get; set; }
    public int TeacherId { get; set; }
    public int Value { get; set; }
}