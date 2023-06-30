using System.Text;

namespace ValGProject
{
    public static class Utility
    {
        public static string Encode(string encodeMe)
        {
            if (string.IsNullOrEmpty(encodeMe))
            {
                return string.Empty;
            }
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        public static string Decode(string? decodeMe)
        {
            if (string.IsNullOrEmpty(decodeMe))
            {
                return string.Empty;
            }
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }
    }
}
