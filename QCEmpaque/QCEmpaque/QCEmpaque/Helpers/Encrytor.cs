namespace QCEmpaque.Helpers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    public class Encryptor
    {
        public static string Encriptar(string dd, string p)
        {
            byte[] k;
            byte[] e = Encoding.UTF8.GetBytes(dd);

            k = Encoding.UTF8.GetBytes(p);

            var td = new TripleDESCryptoServiceProvider();
            td.Key = k;
            td.Mode = CipherMode.ECB;
            td.Padding = PaddingMode.PKCS7;
            ICryptoTransform cryptoTransform = td.CreateEncryptor();
            byte[] r = cryptoTransform.TransformFinalBlock(e, 0, e.Length);
            td.Clear();

            return Convert.ToBase64String(r, 0, r.Length);

        }

        public string Decriptar(string dd, string p)
        {
            byte[] k;
            byte[] d = Convert.FromBase64String(dd);

            k = Encoding.UTF8.GetBytes(p);

            var td = new TripleDESCryptoServiceProvider();
            td.Key = k;
            td.Mode = CipherMode.ECB;
            td.Padding = PaddingMode.PKCS7;

            ICryptoTransform cryptoTransform = td.CreateDecryptor();
            byte[] r = cryptoTransform.TransformFinalBlock(d, 0, d.Length);

            td.Clear();

            return Encoding.UTF8.GetString(r);

        }

        public static byte[] EncriptarPass(string Password)
        {
            byte[] keyArray;
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            keyArray = hashmd5.ComputeHash(encoder.GetBytes(Password));
            return keyArray;
        }
    }
}
