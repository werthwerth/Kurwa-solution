using System.Text;

namespace Final.Static
{
    public class ByteToString
    {
        public static string Convert(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append($"{b:X2}");
            }
            return sb.ToString();
        }
    }
}
