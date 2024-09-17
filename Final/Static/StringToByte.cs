using System.Text;

namespace Final.Static
{
    public class StringToByte
    {
        public static byte[] Convert(string s)
        {
            return Encoding.ASCII.GetBytes(s);
        }
    }
}
