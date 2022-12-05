using System.Text;

namespace TeachersTradeAPI;

public static class Global
{
    public static int DefaultMaxShares { get; set; } = 500;
    public static string CreateMd5(string? input)
    {
        // Use input string to calculate MD5 hash
        using var md5 = System.Security.Cryptography.MD5.Create();
        var inputBytes = md5.ComputeHash(Encoding.ASCII.GetBytes($"SUS{input}AMOGUS is Cool, this is Teachers Trade"));
        inputBytes = md5.ComputeHash(inputBytes);
        inputBytes = md5.ComputeHash(inputBytes);
        return Convert.ToHexString(inputBytes);
    }
}