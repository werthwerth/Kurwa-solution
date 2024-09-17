using System.Security.Cryptography;
using System.Text;

namespace Final.Static
{
    public class RandomString
    {
        public static string Generate()
        {
            return ByteToString.Convert(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString())));
        }
    }
}
