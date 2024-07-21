using System.Security.Cryptography;
using System.Text;

namespace System.Interview.TinyUrl.ByHash.BusinessLogic;

public class HashGenerator
{
    public static string GenerateHash(string url)
    {
        var hashData = MD5.HashData(Encoding.UTF8.GetBytes(url));
        return Convert.ToBase64String(hashData).Substring(0, 4);
    }
}
