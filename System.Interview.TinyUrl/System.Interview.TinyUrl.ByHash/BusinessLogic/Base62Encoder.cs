using System.Text;

namespace System.Interview.TinyUrl.ByHash.BusinessLogic;

public class Base62Encoder
{
    private static readonly char[] base62Chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public static string Encode(int input)
    {
        var sb = new List<char>();
        while (input > 0)
        {
            sb.Add(base62Chars[input % 62]);
            input /= 62;
        }

        sb.Reverse();

        return new string(sb.ToArray());
    }

    public static string Encode(long input)
    {
        var sb = new List<char>();
        while (input > 0)
        {
            sb.Add(base62Chars[input % 62]);
            input /= 62;
        }

        sb.Reverse();

        return new string(sb.ToArray());
    }

    public static string Encode(byte[] input)
    {
        var list = new List<char>();
        var remainder = 0;
        for (int i = input.Length - 1; i >= 0; i--)
        {
            remainder += input[i];
            while (remainder > 62)
            {
                list.Add(base62Chars[remainder % 62]);
                remainder /= 62;
            }
        }

        while (remainder > 0)
        {
            list.Add(base62Chars[remainder % 62]);
            remainder /= 62;
        }

        list.Reverse();

        return new string(list.ToArray());
    }
}
