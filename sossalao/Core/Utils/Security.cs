using System;
using System.Security.Cryptography;
using System.Text;

namespace sossalao.Core.Utils
{
	public class Security
	{
        public string cryptopass(string password)
        {
            byte[] passwordOrigin;
            byte[] modifyPassword;
            MD5 mD5;
            passwordOrigin = Encoding.Default.GetBytes(password);
            mD5 = new MD5CryptoServiceProvider();
            modifyPassword = mD5.ComputeHash(passwordOrigin);
            return Convert.ToBase64String(modifyPassword);
        }
    }
}
