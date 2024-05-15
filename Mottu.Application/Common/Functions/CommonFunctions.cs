using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Desafio.Application.Common.Functions;
public class CommonFunctions
{
    public CommonFunctions()
    {
    }

    public static string GenerateConfirmToken()
    {

        var rand = new Random();
        string result = "";

        for (int ctr = 1; ctr <= 6; ctr++)
        {
            result += rand.Next(1, 10).ToString();
        }

        return result;
    }

    public static string CreateMD5Hash(string input)
    {
        // Step 1, calculate MD5 hash from input
        MD5 md5 = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        // Step 2, convert byte array to hex string
        StringBuilder sb = new();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("X2"));
        }
        return sb.ToString();
    }

    public static bool VerifyAllCharEqual(ref Span<int> input)
    {
        for (var i = 1; i < 11; i++)
        {
            if (input[i] != input[0])
            {
                return false;
            }
        }
        return true;
    }

    public static DateTime ConvertStringToDateTime(string date)
    {
        var culture = new CultureInfo("en-US");
        var ResponseDate = Convert.ToDateTime(date, culture);

        return ResponseDate;
    }
}
