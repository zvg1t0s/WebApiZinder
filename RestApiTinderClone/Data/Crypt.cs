using System.Security.Cryptography;
using System.Text;

namespace RestApiTinderClone.Data
{
    public class Crypt
    {
        public static string hashPassword(string password)
        {
            MD5 md5 = MD5.Create();

            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();

            foreach (var h in hash)
            {
                sb.Append(h.ToString("X2"));

            }
            return Convert.ToString(sb);
        }

    }
}
