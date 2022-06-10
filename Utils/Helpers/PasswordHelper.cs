using System.Security.Cryptography;
using System.Text;

namespace Utils.Helpers
{
    public static class PasswordHelper
	{
		public static string HashPassword(string password)
		{
			using var sha256 = SHA256.Create();
			return Encoding.ASCII.GetString(sha256.ComputeHash(Encoding.ASCII.GetBytes(password)));
		}
	}
}
