namespace HKW.Practices.Public.Security
{
    public static class SecurityFunc
    {
        public static string ConvertStrSecrt(string str)
        {
            if (str == null)
            {
                str = "";
            }
            return GetMD5Hash(str);
        }

        public static string ConvertStrSecrt(string loginname, string password)
        {
            var str = string.Format("{0}:{1}", loginname, password);
            return ConvertStrSecrt(str);
        }

        private static string GetMD5Hash(string input)
        {
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = md5.ComputeHash(bs);
            var s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }
            string password = s.ToString();
            return password;
        }
    }
}
