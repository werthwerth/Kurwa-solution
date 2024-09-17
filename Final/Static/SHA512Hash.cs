using System.Security.Cryptography;

namespace Final.Static
{
    public class SHA512Hash
    {
        public static string Calculate(string data)
        {
            return ByteToString.Convert(SHA512.Create().ComputeHash(StringToByte.Convert(data)));
        }
        public static string Calculate(string data, string salt)
        {
            return ByteToString.Convert(SHA512.Create().ComputeHash(StringToByte.Convert(data+salt)));
        }
    }
}
